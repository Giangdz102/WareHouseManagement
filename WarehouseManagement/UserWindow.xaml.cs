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
using WarehouseManagement.ViewModel;
using WarehouseManagement.Models;
using System.Security.Cryptography;
namespace WarehouseManagement
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        UserViewModel MV = new UserViewModel();
        public UserWindow()
        {
            InitializeComponent();
            LoadedData();
        }

        private void LoadedData()
        {
            LvUser.ItemsSource = MV.GetAllUser();
            //cbRole.ItemsSource = MV.GetAllRoles();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User
                {
                    UserName = txtUserName.Text,
                    IdRole = 2,
                    DisplayName = txtDisplayName.Text,
                    Password = MD5Hash(Base64Encode("123"))
                };
                MV.AddUser(user);
                LoadedData();
                MessageBox.Show("Thêm User thành công!", "Thêm User", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserId.Text))
                {
                    MessageBox.Show("Vui chọn 1 User!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                User user = new User
                {
                    Id = int.Parse(txtUserId.Text),
                    DisplayName = txtDisplayName.Text
                };
                MV.UpdateUser(user);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                Id = int.Parse(txtUserId.Text)
            };
            MV.DeleteUser(user);
            LoadedData();

        }

        private void Button_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("Vui lòng chọn 1 User!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int userId = int.Parse(txtUserId.Text);
            string username = txtUserName.Text;

            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(userId, username);
            changePasswordWindow.ShowDialog();
            LoadedData();
        }
    }
}
