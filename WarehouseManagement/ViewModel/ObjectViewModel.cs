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
    internal class ObjectViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public List<Models.Object> GetAllObject()
        {
            return qlk.Objects
                .Include(o => o.IdUnitNavigation)
                .Include(o => o.IdSuplierNavigation)
                .ToList();
        }

        public List<Unit> GetAllUnits()
        {
            return qlk.Units.ToList();
        }

        public List<Suplier> GetAllSupliers()
        {
            return qlk.Supliers.ToList();
        }
        public void AddObject(Models.Object obj)
        {
            try
            {
                var exitObject = qlk.Objects.FirstOrDefault(o => o.DisplayName == obj.DisplayName);
                if (exitObject != null)
                {
                    MessageBox.Show("Vật tư đã tồn tại!", "Thêm Vật tư thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                qlk.Objects.Add(obj);
                qlk.SaveChanges();
                MessageBox.Show("Thêm vật tư thành công!", "Thêm vật tư", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding object: " + ex.Message);
            }
        }

        public void UpdateObject(Models.Object obj)
        {
            try
            {
                var existingObject = qlk.Objects.FirstOrDefault(o => o.Id == obj.Id);
                if (existingObject != null)
                {
                    existingObject.DisplayName = obj.DisplayName;
                    existingObject.IdUnit = obj.IdUnit;
                    existingObject.IdSuplier = obj.IdSuplier;
                    existingObject.Qrcode = obj.Qrcode;
                    existingObject.BarCode = obj.BarCode;
                    qlk.SaveChanges();
                    MessageBox.Show("Cập nhật vật tư thành công!", "Cập nhật vật tư", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Vật tư không tồn tại!", "Cập nhật vật tư thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating object: " + ex.Message);
            }
        }

        public void DeleteObject(Models.Object obj)
        {
            try
            {
                var existingObject = qlk.Objects.FirstOrDefault(o => o.Id == obj.Id);
                if (existingObject != null)
                {
                    qlk.Objects.Remove(existingObject);
                    qlk.SaveChanges();
                    MessageBox.Show("Xóa vật tư thành công!", "Xóa vật tư", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Vật tư không tồn tại!", "Xóa vật tư thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting object: " + ex.Message);
            }
        } 
    }
}
