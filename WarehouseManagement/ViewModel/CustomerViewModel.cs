using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagement.Models;

namespace WarehouseManagement.ViewModel
{
    internal class CustomerViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public List<Customer> GetAllCustomer()
        {
            return qlk.Customers.ToList();
        }
        public void AddCustomer(Customer customer)
        {
            try
            {
                var extitEmail = qlk.Customers.FirstOrDefault(c => c.Email == customer.Email);

                if (extitEmail != null)
                    {
                        MessageBox.Show("Email đã tồn tại!", "Thêm đơn vị thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                var extitPhone = qlk.Customers.FirstOrDefault(c => c.Phone == customer.Phone);
                if (extitPhone != null)
                    {
                        MessageBox.Show("Số điện thoại đã tồn tại!", "Thêm đơn vị thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                        qlk.Customers.Add(customer);
                        qlk.SaveChanges();
                        
                    
                
            }
            catch(Exception ex)
            {
                throw new Exception("Error adding unit: " + ex.Message);
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = qlk.Customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existingCustomer != null)
                {
                    existingCustomer.DisplayName = customer.DisplayName;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.MoreInfo = customer.MoreInfo;
                    existingCustomer.ContractDate = customer.ContractDate;
                    qlk.SaveChanges();
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Cập nhật khách hàng", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Khách hàng không tồn tại!", "Cập nhật khách hàng thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating unit: " + ex.Message);
            }
        }
        public void DeleteCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = qlk.Customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existingCustomer != null)
                {
                    qlk.Customers.Remove(existingCustomer);
                    qlk.SaveChanges();
                    MessageBox.Show("Xóa khách hàng thành công!", "Xóa khách hàng", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Khách hàng không tồn tại!", "Xóa khách hàng thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting unit: " + ex.Message);
            }
        }
    }
}
