using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagement.Models;

namespace WarehouseManagement.ViewModel
{
    class UnitViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public List<Unit> GetAllUnit()
        {
            return qlk.Units.ToList();
        }
        public void AddUnit(Unit unit)
        {
            try
            {
                foreach (var u in qlk.Units)
                {
                    if (u.DisplayName == unit.DisplayName)
                    {
                        MessageBox.Show("Đơn vị đã tồn tại!", "Thêm đơn vị thất bại", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                qlk.Units.Add(unit);
                qlk.SaveChanges();
                MessageBox.Show("Thêm đơn vị thành công!", "Thêm đơn vị", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding unit: " + ex.Message);
            }
        }
        public void UpdateUnit(Unit unit)
        {
            try
            {
                var existingUnit = qlk.Units.FirstOrDefault(u => u.Id == unit.Id);
                if (existingUnit != null)
                {
                    existingUnit.DisplayName = unit.DisplayName;
                    qlk.SaveChanges();
                    MessageBox.Show("Cập nhật đơn vị thành công!", "Cập nhật đơn vị", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Đơn vị không tồn tại!", "Cập nhật đơn vị thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating unit: " + ex.Message);
            }
        }
        public void DeleteUnit(Unit unit)
        {
            try
            {
                var unitToDelete = qlk.Units.FirstOrDefault(u => u.Id == unit.Id);
                if (unitToDelete != null)
                {
                    qlk.Units.Remove(unitToDelete);
                    qlk.SaveChanges();
                    MessageBox.Show("Xóa đơn vị thành công!", "Xóa đơn vị", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Đơn vị không tồn tại!", "Xóa đơn vị thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting unit: " + ex.Message);
            }
        }
    }
}
