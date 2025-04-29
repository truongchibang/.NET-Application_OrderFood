using Microsoft.EntityFrameworkCore;
using PRN212_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRN212_PROJECT
{
    /// <summary>
    /// Interaction logic for RateOrder.xaml
    /// </summary>
    public partial class RateOrder : Window
    {
        private Account CurrentAccount;
        private int OrderId;

        public RateOrder(Account account, int orderId)
        {
            InitializeComponent();
            CurrentAccount = account;
            OrderId = orderId;
            LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var order = context.Orders
                    .Include(o => o.FoodOrders)
                    .ThenInclude(fo => fo.Food)
                    .FirstOrDefault(o => o.OrderId == OrderId && o.AccountId == CurrentAccount.PhoneNumber && o.Status == 8);

                if (order == null)
                {
                    MessageBox.Show("Đơn hàng không hợp lệ hoặc chưa hoàn thành.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    Customer customer = new Customer(CurrentAccount);
                    customer.Show();
                    this.Close();
                    return;
                }

                DataContext = order;
                OrderItemsControl.ItemsSource = order.FoodOrders;
            }
        }

        private void RateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (RatingComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn điểm đánh giá!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int rating = int.Parse((RatingComboBox.SelectedItem as ComboBoxItem).Content.ToString());
            string comment = CommentTextBox.Text;

            using (var context = new Prn212ProjectBl5Context())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == OrderId && o.AccountId == CurrentAccount.PhoneNumber);
                if (order != null)
                {
                    order.Rate = rating;
                    order.Comment = comment;
                    context.Orders.Update(order);
                    context.SaveChanges();

                    MessageBox.Show("Đánh giá đã được gửi thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    Customer customer = new Customer(CurrentAccount);
                    customer.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn hàng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    Customer customer = new Customer(CurrentAccount);
                    customer.Show();
                    this.Close();
                }
            }
        }
    }
}
