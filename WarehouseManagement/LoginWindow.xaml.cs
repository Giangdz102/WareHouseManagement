using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
using WarehouseManagement.Models;
namespace WarehouseManagement
{
    
    public partial class LoginWindow : Window
    {
        QuanLyKhoContext QLK = new QuanLyKhoContext();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password.Trim();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Email và Mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string adminUsername = "admin";
                string adminPassword = "123";

                if (username == adminUsername && password == adminPassword)
                {
                    labelError.Visibility = Visibility.Hidden;
                    MainWindow adminWindow = new MainWindow();
                    adminWindow.Show();
                    this.Close();
                    
                    return;
                }

                //Customer loggedInCustomer = CM.GetCustomerByEmailAndPassword(email, password);
                //if (loggedInCustomer != null)
                //{
                //    CustomerDashboard customerDashboard = new CustomerDashboard(loggedInCustomer);
                //    this.Close();
                //    customerDashboard.Show();
                //}
                //else
                //{
                //    labelError.Visibility = Visibility.Visible;
                //    txtPassword.Clear();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống khi đăng nhập: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
