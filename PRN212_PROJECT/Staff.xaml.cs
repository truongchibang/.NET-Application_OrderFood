using Microsoft.EntityFrameworkCore;
using PRN212_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        private Account CurrentAccount;
        private List<Order> allOrder;
        string originalPhoneNumber;
        private List<Account> allAccounts;
        private List<TypeFood> allFoodTypes;
        private List<Food> allFoods;
        public Staff(Account account)
        {
            InitializeComponent();
            CurrentAccount = account;
            LoadProfile();
            LoadOrders();
            LoadFood();
            LoadData();
            LoadEmployeeData();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void LoadOrders()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var orders = context.Orders
                    .Include(o => o.StatusNavigation)
                    .Include(o => o.Account)
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
                    NameBox.Text = account.Name;
                    EmailBox.Text = account.Email;
                    PhoneNumberBox.Text = account.PhoneNumber;
                    PasswordTextBox.Text = account.Password;
                    originalPhoneNumber = account.PhoneNumber;
                }

                AddressBox.Text = accRole?.Address ?? string.Empty;
            }
        }
        private void LoadData()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var statuses = context.Statuses.ToList();


                var allStatus = new Status
                {
                    StatusId = -1,
                    Name = "Hiển thị tất cả"
                };


                statuses.Insert(0, allStatus);


                StatusComboBox.ItemsSource = statuses;
                StatusComboBox.DisplayMemberPath = "Name";
                StatusComboBox.SelectedValuePath = "StatusId";
                StatusComboBox.SelectedIndex = 0;

                allOrder = context.Orders.Include(o => o.StatusNavigation).Include(o => o.Account).ToList();
                OrderItemsControl.ItemsSource = allOrder;
            }
        }
        private void ApplyFilters()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var selectedType = StatusComboBox.SelectedItem as Status;
                var searchText = SearchTextBox.Text?.ToLower() ?? "";
                var filtered = allOrder.AsEnumerable();

                if (selectedType != null && selectedType.StatusId == -1)
                {
                    filtered = context.Orders
                        .Include(o => o.StatusNavigation)
                        .Include(o => o.Account)
                        .ToList();
                }

                if (selectedType != null && selectedType.StatusId != -1)
                {
                    filtered = filtered.Where(f => f.Status == selectedType.StatusId);
                }

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    filtered = filtered.Where(x => x.Account.Name != null &&
                                                   x.Account.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                                   x.Account.PhoneNumber != null &&
                                                   x.Account.PhoneNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase));
                }

                OrderItemsControl.ItemsSource = filtered.ToList();
            }
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void UpdateProfile_Click(object sender, RoutedEventArgs e)
        {

            if (PhoneNumberBox.Text != originalPhoneNumber)
            {
                MessageBox.Show("Không được đổi số điện thoại!");
                PhoneNumberBox.Text = originalPhoneNumber;
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
                    account.Name = NameBox.Text;
                    account.Email = EmailBox.Text;
                    account.Password = PasswordTextBox.Text;
                    context.Accounts.Update(account);
                }

                if (accRole != null)
                {
                    accRole.Address = AddressBox.Text;
                    context.AccRoles.Update(accRole);
                }
                else
                {
                    accRole = new AccRole
                    {
                        AccountId = CurrentAccount.PhoneNumber,
                        RoleId = 1,
                        Address = AddressTextBox.Text
                    };
                    context.AccRoles.Add(accRole);
                }

                context.SaveChanges();
                LoadProfile();
                MessageBox.Show("Cập nhật thông tin thành công!");
            }
        }
        private void LoadEmployeeData()
        {
            using (var context = new Prn212ProjectBl5Context())
            {
                var roles = context.Roles
                    .Where(r => r.RoleId == 3 || r.RoleId == 4 || r.RoleId == 5)
                    .ToList();
                var allRolesOption = new Role { RoleId = -1, RoleDefine = "Hiển thị tất cả" };
                roles.Insert(0, allRolesOption);
                RoleComboBox.ItemsSource = roles;
                RoleComboBox.DisplayMemberPath = "RoleDefine";
                RoleComboBox.SelectedValuePath = "RoleId";
                RoleComboBox.SelectedIndex = 0;

                RoleUpdateComboBox.ItemsSource = roles.Where(r => r.RoleId != -1).ToList();
                RoleUpdateComboBox.DisplayMemberPath = "RoleDefine";
                RoleUpdateComboBox.SelectedValuePath = "RoleId";


                var roleAccounts = context.AccRoles
                    .Where(a => a.RoleId == 3 || a.RoleId == 4 || a.RoleId == 5)
                    .Select(a => a.AccountId)
                    .ToList();

                allAccounts = context.Accounts
                    .Where(a => roleAccounts.Contains(a.PhoneNumber))
                    .ToList();

                EmployeeItemsControl.ItemsSource = allAccounts;
            }
        }

        private void ApplyEmployeeFilters()
        {
            var selectedRole = RoleComboBox.SelectedItem as Role;
            var searchText = SearchBox.Text?.ToLower() ?? "";
            var filtered = allAccounts.AsEnumerable();
            if (selectedRole != null && selectedRole.RoleId == -1)
            {
                var context = new Prn212ProjectBl5Context();
                var roleAccounts = context.AccRoles
                    .Where(a => a.RoleId == 3 || a.RoleId == 4 || a.RoleId == 5)
                    .Select(a => a.AccountId)
                    .ToList();

                allAccounts = context.Accounts
                    .Where(a => roleAccounts.Contains(a.PhoneNumber))
                    .ToList();
                filtered = allAccounts;
            }

            if (selectedRole != null && selectedRole.RoleId != -1)
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    var roleAccounts = context.AccRoles
                        .Where(ar => ar.RoleId == selectedRole.RoleId)
                        .Select(ar => ar.AccountId)
                        .ToList();

                    filtered = filtered.Where(a => roleAccounts.Contains(a.PhoneNumber));
                }
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filtered = filtered.Where(a => a.Name != null && a.Name.ToLower().Contains(searchText) || a.PhoneNumber != null && a.PhoneNumber.ToLower().Contains(searchText));
            }

            EmployeeItemsControl.ItemsSource = filtered.ToList();
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEmployeeFilters();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyEmployeeFilters();
        }

        private void SelectEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string accountId)
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    var account = context.Accounts
                        .FirstOrDefault(a => a.PhoneNumber == accountId);
                    var role = context.AccRoles
                        .FirstOrDefault(a => a.AccountId == accountId);

                    if (account != null && role != null)
                    {
                        NameTextBox.Text = account.Name;
                        EmailTextBox.Text = account.Email;
                        PhoneNumberTextBox.Text = account.PhoneNumber;
                        AddressTextBox.Text = role.Address ?? "";
                        RoleUpdateComboBox.SelectedItem = RoleUpdateComboBox.Items
                            .Cast<Role>()
                            .FirstOrDefault(r => r.RoleId == role.RoleId);
                    }
                }
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) || RoleUpdateComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin (Tên, Email, Số điện thoại, Vai trò)!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new Prn212ProjectBl5Context())
            {
                if (context.Accounts.Any(a => a.PhoneNumber == PhoneNumberTextBox.Text))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var account = new Account
                {
                    Name = NameTextBox.Text,
                    Email = EmailTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Password = "12345678",
                };

                var roleId = (RoleUpdateComboBox.SelectedItem as Role).RoleId;
                context.Accounts.Add(account);
                context.SaveChanges();
                var accRole = new AccRole
                {
                    AccountId = account.PhoneNumber,
                    RoleId = roleId,
                    Address = AddressTextBox.Text,
                    Account = account,
                    Role = context.Roles.FirstOrDefault(x => x.RoleId == roleId)
                };


                context.AccRoles.Add(accRole);
                context.SaveChanges();

                LoadEmployeeData();
                NameTextBox.Text = "";
                EmailTextBox.Text = "";
                PhoneNumberTextBox.Text = "";
                AddressTextBox.Text = "";
                RoleUpdateComboBox.SelectedIndex = -1;
                MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    var account = context.AccRoles
                        .Include(a => a.Account)
                        .FirstOrDefault(a => a.AccountId == PhoneNumberTextBox.Text);

                    if (account == null)
                    {
                        MessageBox.Show("Nhân viên không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    context.AccRoles.RemoveRange(account);
                    context.Accounts.Remove(account.Account);
                    context.SaveChanges();

                    LoadEmployeeData();
                    NameTextBox.Text = "";
                    EmailTextBox.Text = "";
                    PhoneNumberTextBox.Text = "";
                    AddressTextBox.Text = "";
                    RoleUpdateComboBox.SelectedIndex = -1;
                    MessageBox.Show("Xóa nhân viên thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void UpdateRole_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) || RoleUpdateComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên và vai trò để cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new Prn212ProjectBl5Context())
            {
                var accRole = context.AccRoles
                    .FirstOrDefault(ar => ar.AccountId == PhoneNumberTextBox.Text);

                if (accRole == null)
                {
                    MessageBox.Show("Nhân viên không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                accRole.RoleId = (RoleUpdateComboBox.SelectedItem as Role).RoleId;
                context.AccRoles.Update(accRole);
                context.SaveChanges();

                LoadEmployeeData();
                MessageBox.Show("Cập nhật vai trò thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ResetFields_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            AddressTextBox.Text = "";
            RoleUpdateComboBox.SelectedIndex = -1;
        }
        private void LoadFood()
        {
            try
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    allFoodTypes = context.TypeFoods.ToList();
                    allFoods = context.Foods.Include(f => f.Typef).ToList();
                }

                var allType = new TypeFood { TypefId = -1, Typename = "Hiển thị tất cả" };
                allFoodTypes.Insert(0, allType);
                FoodTypeFilterComboBox.ItemsSource = allFoodTypes;
                FoodTypeFilterComboBox.DisplayMemberPath = "Typename";
                FoodTypeFilterComboBox.SelectedValuePath = "TypefId";
                FoodTypeFilterComboBox.SelectedIndex = 0;
                FoodTypeComboBox.ItemsSource = allFoodTypes.Where(t => t.TypefId != -1).ToList();
                FoodTypeComboBox.DisplayMemberPath = "Typename";
                FoodTypeComboBox.SelectedValuePath = "TypefId";
                FoodItemsControl.ItemsSource = allFoods;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu thực phẩm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ApplyFoodFilters()
        {
            if (allFoods == null)
            {
                MessageBox.Show("Không thể áp dụng, dữ liệu thực phẩm chưa được tải.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedType = FoodTypeFilterComboBox.SelectedItem as TypeFood;
            var searchText = FoodSearchBox.Text?.ToLower() ?? "";
            var filtered = allFoods.AsEnumerable();
            if (selectedType != null && selectedType.TypefId == -1)
            {
                filtered = allFoods;
            }
            else if (selectedType != null && selectedType.TypefId != -1)
            {
                filtered = filtered.Where(f => f.TypefId == selectedType.TypefId);
            }
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filtered = filtered.Where(f => f.FoodName.ToLower().Contains(searchText));
            }
            FoodItemsControl.ItemsSource = filtered.ToList();
        }
        private void FoodTypeFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFoodFilters();
        }

        private void FoodSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFoodFilters();
        }
        private void SelectFood_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int foodId)
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    var food = context.Foods.Include(f => f.Typef).FirstOrDefault(f => f.FoodId == foodId);
                    if (food != null)
                    {
                        FoodIdTextBox.Text = food.FoodId.ToString();
                        FoodNameTextBox.Text = food.FoodName;
                        FoodPriceTextBox.Text = food.Price.ToString();
                        FoodImageTextBox.Text = food.Image;
                        FoodTypeComboBox.SelectedItem = allFoodTypes.FirstOrDefault(t => t.TypefId == food.TypefId);
                    }
                }
            }
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodNameTextBox.Text) || string.IsNullOrWhiteSpace(FoodPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(FoodImageTextBox.Text) || FoodTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(FoodPriceTextBox.Text, out int price))
            {
                MessageBox.Show("Giá phải là số nguyên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (var context = new Prn212ProjectBl5Context())
            {
                var food = new Food
                {
                    FoodName = FoodNameTextBox.Text,
                    TypefId = (FoodTypeComboBox.SelectedItem as TypeFood).TypefId,
                    Price = price,
                    Image = FoodImageTextBox.Text
                };
                context.Foods.Add(food);
                context.SaveChanges();
                allFoods = context.Foods.Include(f => f.Typef).ToList();
                ApplyFoodFilters();
                FoodIdTextBox.Text = "";
                FoodNameTextBox.Text = "";
                FoodPriceTextBox.Text = "";
                FoodImageTextBox.Text = "";
                FoodTypeComboBox.SelectedIndex = -1;
                MessageBox.Show("Thêm thực phẩm thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void UpdateFood_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodIdTextBox.Text))
            {
                MessageBox.Show("Vui lòng chọn thực phẩm để cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(FoodIdTextBox.Text, out int foodId))
            {
                MessageBox.Show("ID thực phẩm không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(FoodNameTextBox.Text) || string.IsNullOrWhiteSpace(FoodPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(FoodImageTextBox.Text) || FoodTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(FoodPriceTextBox.Text, out int price))
            {
                MessageBox.Show("Giá phải là số nguyên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new Prn212ProjectBl5Context())
            {
                var food = context.Foods.Find(foodId);
                if (food == null)
                {
                    MessageBox.Show("Thực phẩm không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                food.FoodName = FoodNameTextBox.Text;
                food.TypefId = (FoodTypeComboBox.SelectedItem as TypeFood).TypefId;
                food.Price = price;
                food.Image = FoodImageTextBox.Text;

                context.Foods.Update(food);
                context.SaveChanges();

                allFoods = context.Foods.Include(f => f.Typef).ToList();
                ApplyFoodFilters();
                MessageBox.Show("Cập nhật thực phẩm thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                FoodIdTextBox.Text = "";
                FoodNameTextBox.Text = "";
                FoodPriceTextBox.Text = "";
                FoodImageTextBox.Text = "";
                FoodTypeComboBox.SelectedIndex = -1;
            }
        }

        private void DeleteFood_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodIdTextBox.Text))
            {
                MessageBox.Show("Vui lòng chọn thực phẩm để xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(FoodIdTextBox.Text, out int foodId))
            {
                MessageBox.Show("ID thực phẩm không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa thực phẩm này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (var context = new Prn212ProjectBl5Context())
                {
                    var food = context.Foods.Find(foodId);
                    if (food == null)
                    {
                        MessageBox.Show("Thực phẩm không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    context.Foods.Remove(food);
                    context.SaveChanges();

                    allFoods = context.Foods.Include(f => f.Typef).ToList();
                    ApplyFoodFilters();
                    FoodIdTextBox.Text = "";
                    FoodNameTextBox.Text = "";
                    FoodPriceTextBox.Text = "";
                    FoodImageTextBox.Text = "";
                    FoodTypeComboBox.SelectedIndex = -1;
                    MessageBox.Show("Xóa thực phẩm thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ResetFoodFields_Click(object sender, RoutedEventArgs e)
        {
            FoodIdTextBox.Text = "";
            FoodNameTextBox.Text = "";
            FoodPriceTextBox.Text = "";
            FoodImageTextBox.Text = "";
            FoodTypeComboBox.SelectedIndex = -1;
        }
    }
}
