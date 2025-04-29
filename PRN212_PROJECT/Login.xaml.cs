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
    /// Interaction logic for LoginC.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void HyperlinkRegister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtPhoneNumber.Text== null||pwdBox.Password== null)
            {
                MessageBox.Show("Bạn hãy nhập đầy đủ thông tin.");
                return;
            }
            var context = new Prn212ProjectBl5Context();
            var account = context.Accounts.FirstOrDefault(x=>x.PhoneNumber == txtPhoneNumber.Text);
            if (account == null)
            {
                MessageBox.Show("Bạn hãy kiểm tra lại số điện thoại của mình");
                return;
            }
            else
            {
                if (account.Password != pwdBox.Password)
                {
                    MessageBox.Show("Bạn nhập sai mật khẩu vui lòng kiểm tra lại");
                    return;
                }
                else
                {
                    var role = context.AccRoles.FirstOrDefault(x => x.AccountId == txtPhoneNumber.Text);
                    if (role.RoleId == 1)
                    {
                        Staff staff = new Staff(account);
                        staff.Show();
                        this.Close();
                    }
                    else if (role.RoleId == 2)
                    {
                        Customer customer = new Customer(account);
                        customer.Show();
                        this.Close();
                    }
                    else if (role.RoleId == 3)
                    {
                        KitchenEmploy kitchenEmploy = new KitchenEmploy(account);
                        kitchenEmploy.Show();
                        this.Close();
                    }
                    else if (role.RoleId == 4)
                    {
                        OrderEmploy orderEmploy = new OrderEmploy(account);
                        orderEmploy.Show();
                        this.Close();
                    }
                    else
                    {
                        Shipper shipper = new Shipper(account);
                        shipper.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}
