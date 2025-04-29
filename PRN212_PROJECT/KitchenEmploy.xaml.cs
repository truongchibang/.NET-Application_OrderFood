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
    /// Interaction logic for KitchenEmploy.xaml
    /// </summary>
    public partial class KitchenEmploy : Window
    {
        private Account CurrentAccount;
        string originalPhoneNumber;
        public KitchenEmploy(Account account)
        {
            InitializeComponent();
            CurrentAccount = account;
            LoadProfile();
            LoadOrders();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void LoadProfile()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var account = context.Accounts
                    .FirstOrDefault(a => a.PhoneNumber == CurrentAccount.PhoneNumber);
                var accRole = context.AccRoles
                    .FirstOrDefault(ar => ar.AccountId == CurrentAccount.PhoneNumber);

                if (account != null)
                {
                    NameTextBox.Text = account.Name;
                    EmailTextBox.Text = account.Email;
                    PhoneNumberTextBox.Text = account.PhoneNumber;
                    PasswordTextBox.Text = account.Password;
                    originalPhoneNumber = account.PhoneNumber;
                }

                AddressTextBox.Text = accRole?.Address ?? string.Empty;
            }
        }

        private void UpdateProfile_Click(object sender, RoutedEventArgs e)
        {

            if (PhoneNumberTextBox.Text != originalPhoneNumber)
            {
                MessageBox.Show("Không được đổi số điện thoại!");
                PhoneNumberTextBox.Text = originalPhoneNumber;
                return;
            }

            using (var context = new Prn212ProjectBl5Context())
            {
                var account = context.Accounts
                    .FirstOrDefault(a => a.PhoneNumber == CurrentAccount.PhoneNumber);
                var accRole = context.AccRoles
                    .FirstOrDefault(ar => ar.AccountId == CurrentAccount.PhoneNumber);

                if (account != null)
                {
                    account.Name = NameTextBox.Text;
                    account.Email = EmailTextBox.Text;
                    account.Password = PasswordTextBox.Text;
                    context.Accounts.Update(account);
                }

                if (accRole != null)
                {
                    accRole.Address = AddressTextBox.Text;
                    context.AccRoles.Update(accRole);
                }
                else
                {
                    accRole = new AccRole
                    {
                        AccountId = CurrentAccount.PhoneNumber,
                        RoleId = 3,
                        Address = AddressTextBox.Text
                    };
                    context.AccRoles.Add(accRole);
                }

                context.SaveChanges();
                LoadProfile();
                MessageBox.Show("Cập nhật thông tin thành công!");
            }
        }
        private void LoadOrders()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var orders = context.Orders
                    .Where(o => o.Status == 3)
                    .Select(o => new
                    {
                        o.OrderId,
                        o.AccountId,
                        o.TimeOder,
                        o.Total,
                        Food_orders = o.FoodOrders.Select(fo => new
                        {
                            fo.Food.FoodName,
                            fo.Quantity
                        }).ToList()
                    })
                    .ToList();

                OrderList.ItemsSource = orders;
            }
        }

        private void FinishCookOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int orderId)
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null && order.Status == 3)
                    {
                        order.Status = 4;
                        context.SaveChanges();
                        LoadOrders();
                        MessageBox.Show("Đã hoàn thành đơn hàng!");
                    }
                }
            }
        }
    }
}
