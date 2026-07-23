using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagement.Models;

namespace WarehouseManagement.ViewModel
{
    internal class SuplierViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public List<Suplier> GetAllSuplier()
        {
            return qlk.Supliers.ToList();
        }
        public void AddSuplier(Suplier suplier)
        {
            try
            {
                var extitEmail = qlk.Supliers.FirstOrDefault(c => c.Email == suplier.Email);

                if (extitEmail != null)
                {
                    MessageBox.Show("Email đã tồn tại!", "Thêm đơn vị thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var extitPhone = qlk.Supliers.FirstOrDefault(c => c.Phone == suplier.Phone);
                if (extitPhone != null)
                {
                    MessageBox.Show("Số điện thoại đã tồn tại!", "Thêm đơn vị thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                qlk.Supliers.Add(suplier);
                qlk.SaveChanges();



            }
            catch (Exception ex)
            {
                throw new Exception("Error adding unit: " + ex.Message);
            }
        }
        public void UpdateSuplier(Suplier suplier)
        {
            try
            {
                var existingSuplier = qlk.Supliers.FirstOrDefault(c => c.Id == suplier.Id);
                if (existingSuplier != null)
                {
                    existingSuplier.DisplayName = suplier.DisplayName;
                    existingSuplier.Address = suplier.Address;
                    existingSuplier.Phone = suplier.Phone;
                    existingSuplier.Email = suplier.Email;
                    existingSuplier.MoreInfo = suplier.MoreInfo;
                    existingSuplier.ContractDate = suplier.ContractDate;
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
        public void DeleteSuplier(Suplier suplier)
        {
            try
            {
                var existingSuplier = qlk.Supliers.FirstOrDefault(c => c.Id == suplier.Id);
                if (existingSuplier != null)
                {
                    qlk.Supliers.Remove(existingSuplier);
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
