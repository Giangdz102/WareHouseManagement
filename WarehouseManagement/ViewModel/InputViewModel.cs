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
    class InputViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();

        public List<InputInfo> GetAllInputInfo()
        {
            return qlk.InputInfos
                .Include(i => i.IdObjectNavigation)
                .Include(i => i.IdInputNavigation)
                .ToList();
        }

        public List<Models.Object> GetAllObjects()
        {
            return qlk.Objects.ToList();
        }

        public void AddInput(Input input, InputInfo inputInfo)
        {
            using var transaction = qlk.Database.BeginTransaction();
            try
            {
                qlk.Inputs.Add(input);
                qlk.SaveChanges();

                inputInfo.IdInput = input.Id;
                qlk.InputInfos.Add(inputInfo);
                qlk.SaveChanges();

                transaction.Commit();
                MessageBox.Show("Thêm phiếu nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Lỗi khi thêm phiếu nhập: " + ex.Message);
            }
        }

        public void UpdateInput(Input input, InputInfo inputInfo)
        {
            using var transaction = qlk.Database.BeginTransaction();
            try
            {
                var dbInput = qlk.Inputs.FirstOrDefault(i => i.Id == input.Id);
                if (dbInput != null)
                {
                    dbInput.DateInput = input.DateInput;
                }

                var dbInputInfo = qlk.InputInfos.FirstOrDefault(ii => ii.Id == inputInfo.Id);
                if (dbInputInfo != null)
                {
                    dbInputInfo.IdObject = inputInfo.IdObject;
                    dbInputInfo.IdInput = inputInfo.IdInput;
                    dbInputInfo.Count = inputInfo.Count;
                    dbInputInfo.InputPrice = inputInfo.InputPrice;
                    dbInputInfo.OutputPrice = inputInfo.OutputPrice;
                    dbInputInfo.Status = inputInfo.Status;
                }

                qlk.SaveChanges();
                transaction.Commit();
                MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Lỗi khi cập nhật phiếu nhập: " + ex.Message);
            }
        }

        public void DeleteInput(int id)
        {
            using var transaction = qlk.Database.BeginTransaction();
            try
            {
                var dbInputInfo = qlk.InputInfos.FirstOrDefault(ii => ii.Id == id);
                if (dbInputInfo != null)
                {
                    int inputId = dbInputInfo.IdInput;
                    qlk.InputInfos.Remove(dbInputInfo);
                    qlk.SaveChanges();

                    var dbInput = qlk.Inputs.FirstOrDefault(i => i.Id == inputId);
                    if (dbInput != null)
                    {
                        qlk.Inputs.Remove(dbInput);
                        qlk.SaveChanges();
                    }
                }

                transaction.Commit();
                MessageBox.Show("Xóa phiếu nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Lỗi khi xóa phiếu nhập: " + ex.Message);
            }
        }
    }
}
