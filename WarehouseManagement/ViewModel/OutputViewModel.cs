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
    class OutputViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();

        public List<OutputInfo> GetAllOutputInfo()
        {
            return qlk.OutputInfos
                .Include(o => o.IdObjectNavigation)
                    .ThenInclude(ob => ob.InputInfos)
                .Include(o => o.IdCustomerNavigation)
                .Include(o => o.IdOutputInfoNavigation)
                .ToList();
        }

        public List<Models.Object> GetAllObjects()
        {
            return qlk.Objects.ToList();
        }

        public List<Customer> GetAllCustomers()
        {
            return qlk.Customers.ToList();
        }

        public InputInfo GetInputInfoForObject(int objectId)
        {
            return qlk.InputInfos.FirstOrDefault(ii => ii.IdObject == objectId);
        }

        public int GetStockForObject(int objectId, int excludeOutputId = 0)
        {
            int sumInput = qlk.InputInfos.Where(ii => ii.IdObject == objectId).Sum(ii => ii.Count) ?? 0;
            
            // If editing, exclude the count of the output being edited from the calculation
            int sumOutput = qlk.OutputInfos
                .Where(oi => oi.IdObject == objectId && oi.Id != excludeOutputId)
                .Sum(oi => oi.Count) ?? 0;

            return sumInput - sumOutput;
        }

        public void AddOutput(Output output, OutputInfo outputInfo)
        {
            using var transaction = qlk.Database.BeginTransaction();
            try
            {
                qlk.Outputs.Add(output);
                qlk.SaveChanges();

                outputInfo.IdOutputInfo = output.Id;
                qlk.OutputInfos.Add(outputInfo);
                qlk.SaveChanges();

                transaction.Commit();
                MessageBox.Show("Thêm phiếu xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Lỗi khi thêm phiếu xuất: " + ex.Message);
            }
        }

        public void UpdateOutput(Output output, OutputInfo outputInfo)
        {
            using var transaction = qlk.Database.BeginTransaction();
            try
            {
                var dbOutput = qlk.Outputs.FirstOrDefault(o => o.Id == output.Id);
                if (dbOutput != null)
                {
                    dbOutput.DateOutput = output.DateOutput;
                }

                var dbOutputInfo = qlk.OutputInfos.FirstOrDefault(oi => oi.Id == outputInfo.Id);
                if (dbOutputInfo != null)
                {
                    dbOutputInfo.IdObject = outputInfo.IdObject;
                    dbOutputInfo.IdCustomer = outputInfo.IdCustomer;
                    dbOutputInfo.IdOutputInfo = outputInfo.IdOutputInfo;
                    dbOutputInfo.Count = outputInfo.Count;
                    dbOutputInfo.Status = outputInfo.Status;
                    dbOutputInfo.PriceOutput = outputInfo.PriceOutput;
                }

                qlk.SaveChanges();
                transaction.Commit();
                MessageBox.Show("Cập nhật phiếu xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Lỗi khi cập nhật phiếu xuất: " + ex.Message);
            }
        }

        public void DeleteOutput(int id)
        {
            using var transaction = qlk.Database.BeginTransaction();
            try
            {
                var dbOutputInfo = qlk.OutputInfos.FirstOrDefault(oi => oi.Id == id);
                if (dbOutputInfo != null)
                {
                    int outputId = dbOutputInfo.IdOutputInfo;
                    qlk.OutputInfos.Remove(dbOutputInfo);
                    qlk.SaveChanges();

                    var dbOutput = qlk.Outputs.FirstOrDefault(o => o.Id == outputId);
                    if (dbOutput != null)
                    {
                        qlk.Outputs.Remove(dbOutput);
                        qlk.SaveChanges();
                    }
                }

                transaction.Commit();
                MessageBox.Show("Xóa phiếu xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Lỗi khi xóa phiếu xuất: " + ex.Message);
            }
        }
    }
}
