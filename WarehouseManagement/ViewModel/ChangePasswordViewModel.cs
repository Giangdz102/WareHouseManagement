using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagement.Models;
using System.Security.Cryptography;
namespace WarehouseManagement.ViewModel
{
    internal class ChangePasswordViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public bool ChangePassword(int userId, string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ tất cả các trường!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Xác nhận mật khẩu mới không khớp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (newPassword == oldPassword)
                {
                    MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var user = qlk.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    MessageBox.Show("Người dùng không tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // Check old password
                string oldPasswordHash = MD5Hash(Base64Encode(oldPassword));
                if (user.Password != oldPasswordHash)
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // Update to new password
                user.Password = MD5Hash(Base64Encode(newPassword));
                qlk.SaveChanges();

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đổi mật khẩu: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
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
    }
}
