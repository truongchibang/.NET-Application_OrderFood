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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pwdBoxReg.Password == null || pwdBoxConfirm.Password == null || txtAddress.Text == null || txtEmailReg.Text == null || txtPhone.Text == null || txtUsernameReg.Text == null)
            {
                MessageBox.Show("Bạn hãy nhập đầy đủ thông tin");
                return;
            }
            if (pwdBoxConfirm.Password != pwdBoxReg.Password)
            {
                MessageBox.Show("Password không trùng với confirm,vui lòng nhập lại.");
                return;
            }
            if (pwdBoxReg.Password.Length < 8 || pwdBoxReg.Password.Length > 16)
            {
                MessageBox.Show("Bạn hãy nhập mật khẩu có độ dài 8-16 kí tự");
                return;
            }
            using var context = new Prn212ProjectBl5Context();
            var accountT = context.Accounts.FirstOrDefault(x => x.PhoneNumber == txtPhone.Text);
            if (accountT != null)
            {
                MessageBox.Show("Bạn không thể dùng số điện thoại này vì nó đã có trong hệ thống.");
                return;
            }
            Account account = new Account
            {
                PhoneNumber = txtPhone.Text,
                Email = txtEmailReg.Text,
                Name = txtUsernameReg.Text,
                Password = pwdBoxReg.Password
            };
            context.Accounts.Add(account);
            context.SaveChanges();
            AccRole accRole = new AccRole
            {
                AccountId = txtPhone.Text,
                RoleId = 2,
                Address = txtAddress.Text,
                Account = account,
                Role = context.Roles.FirstOrDefault(x=>x.RoleId ==2)
            };

            context.AccRoles.Add(accRole);
            context.SaveChanges();
            MessageBox.Show("Tạo tài khoản thành công.");
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void HyperlinkLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
