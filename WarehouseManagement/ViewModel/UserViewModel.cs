using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagement.Models;

namespace WarehouseManagement.ViewModel
{
    internal class UserViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public List<User> GetAllUser()
        {
            return qlk.Users.Include(u => u.IdRoleNavigation).ToList();
        }
        public List<UserRole> GetAllRoles()
        {
            return qlk.UserRoles.ToList();
        }
        public void UpdateUser(User user)
        {
            try
            {
                var existingUser = qlk.Users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    existingUser.DisplayName = user.DisplayName;
                    qlk.SaveChanges();
                    System.Windows.MessageBox.Show("Cập nhật tên hiển thị thành công!", "Cập nhật người dùng", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Người dùng không tồn tại!", "Cập nhật thất bại", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }
        public void AddUser(User user)
        {
            try
            {
                var exitAccount = qlk.Users.FirstOrDefault(u => u.UserName == user.UserName);
                if (exitAccount != null)
                {
                    MessageBox.Show("Tài khoản đã tồn tại!", "Thêm tài khoản thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                qlk.Users.Add(user);
                qlk.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }

        }
        public void DeleteUser(User user)
        {
            try
            {
                var existingUser = qlk.Users.FirstOrDefault(c => c.Id == user.Id);
                if (existingUser != null)
                {
                    if(existingUser.IdRole == 1)
                    {
                        MessageBox.Show("Không thể xóa admin!", "Xóa User thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    qlk.Users.Remove(existingUser);
                    qlk.SaveChanges();
                    MessageBox.Show("Xóa User thành công!", "Xóa User", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("User không tồn tại!", "Xóa khách hàng thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting unit: " + ex.Message);
            }
        } 
    }
}
