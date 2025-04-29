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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        private Account CurrentAccount;
        private List<Food> allFoods;
        string originalPhoneNumber;

        public Customer(Account account)
        {
            InitializeComponent();
            CurrentAccount = account;
            this.Loaded += Window_Loaded;
            LoadProfile();
            LoadCartItems();
            LoadOrders();
            LoadOrdersShip();
            LoadOrdersFinish();
            var context = new Prn212ProjectBl5Context();
            var orders = context.Orders
                    .Where(o => o.Status == 7 && o.AccountId == CurrentAccount.PhoneNumber).ToList();
            if (orders != null)
            {
                MessageBox.Show("Đơn hàng của bạn đã đến, bạn vui lòng ra nhận hàng giúp shop ạ!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var foodTypes = context.TypeFoods.ToList();


                var allTypesOption = new TypeFood
                {
                    TypefId = -1,
                    Typename = "Hiển thị tất cả"
                };


                foodTypes.Insert(0, allTypesOption);


                FoodTypeComboBox.ItemsSource = foodTypes;
                FoodTypeComboBox.DisplayMemberPath = "Typename";
                FoodTypeComboBox.SelectedValuePath = "TypefId";
                FoodTypeComboBox.SelectedIndex = 0;

                allFoods = context.Foods.ToList();
                FoodItemsControl.ItemsSource = allFoods;
            }
        }

        private void ApplyFilters()
        {
            var context = new Prn212ProjectBl5Context();
            var selectedType = FoodTypeComboBox.SelectedItem as TypeFood;
            var searchText = SearchTextBox.Text?.ToLower() ?? "";
            var filtered = allFoods.AsEnumerable();
            if (selectedType.TypefId == -1) filtered = context.Foods.ToList();

            if (selectedType != null && selectedType.TypefId != -1)
                filtered = filtered.Where(f => f.TypefId == selectedType.TypefId);

            if (!string.IsNullOrWhiteSpace(searchText))
                filtered = filtered.Where(f => f.FoodName.ToLower().Contains(searchText));

            FoodItemsControl.ItemsSource = filtered.ToList();
        }

        private void FoodTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int foodId = (int)button.Tag;
            using (var context = new Prn212ProjectBl5Context())
            {
                var cart = context.Carts.FirstOrDefault(c => c.AccountId == CurrentAccount.PhoneNumber);
                if (cart != null)
                {
                    var foodCart = context.FoodCarts.FirstOrDefault(x => x.CartId == cart.CartId && x.FoodId == foodId);
                    if (foodCart == null)
                    {
                        foodCart = new FoodCart
                        {
                            CartId = cart.CartId,
                            FoodId = foodId,
                            Quantity = 1
                        };
                        context.FoodCarts.Add(foodCart);
                        context.SaveChanges();
                        MessageBox.Show("Đã thêm vào giỏ hàng!");
                    }
                    else
                    {
                        foodCart.Quantity++;
                        context.FoodCarts.Update(foodCart);
                        context.SaveChanges();
                    }
                }
                else
                {
                    Cart cart1 = new Cart { AccountId = CurrentAccount.PhoneNumber };
                    context.Carts.Add(cart1);
                    context.SaveChanges();
                    var foodCart = new FoodCart
                    {
                        CartId = cart1.CartId,
                        FoodId = foodId,
                        Quantity = 1
                    };
                    context.FoodCarts.Add(foodCart);
                    context.SaveChanges();
                    MessageBox.Show("Đã thêm vào giỏ hàng!");
                }
            }
        }
        private void LoadCartItems()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var cart = context.Carts.FirstOrDefault(c => c.AccountId == CurrentAccount.PhoneNumber);
                if (cart != null)
                {
                    var cartItems = context.FoodCarts
                        .Include(fc => fc.Food)
                        .Where(fc => fc.CartId == cart.CartId)
                        .ToList();
                    CartItemsControl.ItemsSource = cartItems;
                    UpdateTotalPrice(cartItems);
                }
                else
                {
                    CartItemsControl.ItemsSource = null;
                    TotalPriceTextBlock.Text = "Tổng tiền: 0 VND";
                }
            }
        }

        private void UpdateTotalPrice(List<FoodCart> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
            {
                TotalPriceTextBlock.Text = "Tổng tiền: 0 VND";
                return;
            }

            int totalPrice = cartItems.Sum(fc => fc.Food.Price * fc.Quantity);
            TotalPriceTextBlock.Text = $"Tổng tiền: {totalPrice:N0} VND";
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int foodCartId = (int)button.Tag;
            using (var context = new Prn212ProjectBl5Context())
            {
                var foodCart = context.FoodCarts.Include(fc => fc.Food).FirstOrDefault(fc => fc.FoodCart1 == foodCartId);
                if (foodCart != null)
                {
                    foodCart.Quantity++;
                    context.FoodCarts.Update(foodCart);
                    context.SaveChanges();
                    LoadCartItems();
                }
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int foodCartId = (int)button.Tag;
            using (var context = new Prn212ProjectBl5Context())
            {
                var foodCart = context.FoodCarts.Include(fc => fc.Food).FirstOrDefault(fc => fc.FoodCart1 == foodCartId);
                if (foodCart != null)
                {
                    foodCart.Quantity--;
                    if (foodCart.Quantity <= 0)
                    {
                        context.FoodCarts.Remove(foodCart);
                    }
                    else
                    {
                        context.FoodCarts.Update(foodCart);
                    }
                    context.SaveChanges();
                    LoadCartItems();
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int foodCartId = (int)button.Tag;
            using (var context = new Prn212ProjectBl5Context())
            {
                var foodCart = context.FoodCarts.Include(fc => fc.Food).FirstOrDefault(fc => fc.FoodCart1 == foodCartId);
                if (foodCart != null)
                {
                    context.FoodCarts.Remove(foodCart);
                    context.SaveChanges();
                    LoadCartItems();
                }
            }
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var cart = context.Carts.FirstOrDefault(c => c.AccountId == CurrentAccount.PhoneNumber);
                if (cart == null || !context.FoodCarts.Any(fc => fc.CartId == cart.CartId))
                {
                    MessageBox.Show("Giỏ hàng trống!");
                    return;
                }

                var cartItems = context.FoodCarts
                    .Include(fc => fc.Food)
                    .Where(fc => fc.CartId == cart.CartId)
                    .ToList();

                int totalPrice = cartItems.Sum(fc => fc.Food.Price * fc.Quantity);


                var order = new Order
                {
                    AccountId = CurrentAccount.PhoneNumber,
                    Status = 1,
                    Rate = 0,
                    Comment = "",
                    TimeOder = DateTime.Now,
                    TimeFinish = null,
                    Total = totalPrice
                };
                context.Orders.Add(order);
                context.SaveChanges();

                foreach (var item in cartItems)
                {
                    var foodOrder = new FoodOrder
                    {
                        OrderId = order.OrderId,
                        FoodId = item.FoodId,
                        Quantity = item.Quantity
                    };
                    context.FoodOrders.Add(foodOrder);
                }

                context.FoodCarts.RemoveRange(cartItems);
                context.Carts.Remove(cart);
                context.SaveChanges();

                LoadCartItems();
                MessageBox.Show("Đơn hàng đã được chốt thành công!");
            }
        }
        private void LoadOrders()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var orders = context.Orders
                    .Include(o => o.StatusNavigation)
                    .Where(o => o.AccountId == CurrentAccount.PhoneNumber)
                    .ToList();
                OrderItemsControl.ItemsSource = orders;
            }
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
                        RoleId = 2,
                        Address = AddressTextBox.Text
                    };
                    context.AccRoles.Add(accRole);
                }

                context.SaveChanges();
                LoadProfile();
                MessageBox.Show("Cập nhật thông tin thành công!");
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl == null) return;

            if (tabControl.SelectedIndex == 1)
            {
                LoadCartItems();
            }
            else if (tabControl.SelectedIndex == 2)
            {
                LoadOrdersShip();
            }
            else if (tabControl.SelectedIndex == 3)
            {
                LoadOrdersFinish();
            }
            else if (tabControl.SelectedIndex == 4)
            {
                LoadOrders();
            }
            else if (tabControl.SelectedIndex == 5)
            {
                LoadProfile();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void LoadOrdersShip()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var orders = context.Orders
                    .Where(o => o.Status == 7 && o.AccountId == CurrentAccount.PhoneNumber)
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

        private void AcceptOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int orderId)
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null && order.Status == 7)
                    {
                        order.TimeFinish = DateTime.Now;
                        order.Status = 8;
                        context.SaveChanges();
                        LoadOrdersShip();
                        MessageBox.Show("Đã nhận đơn hàng!");
                    }
                }
            }
        }
        private void LoadOrdersFinish()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var orders = context.Orders
                    .Where(o => o.Status == 8 && o.AccountId == CurrentAccount.PhoneNumber)
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

                OrderFinishList.ItemsSource = orders;
            }
        }
        private void RateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int orderId)
            {
                RateOrder rateOrder = new RateOrder(CurrentAccount, orderId);
                rateOrder.Show();
                this.Hide();
            }
        }
    }
}
