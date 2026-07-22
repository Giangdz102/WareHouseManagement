using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagement.Models;
using System.Security.Cryptography;
using System.Collections.ObjectModel;

namespace WarehouseManagement.ViewModel
{
    internal class MainViewModel
    {     
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public List<TonKho> TonKhoData()
        {
            var result = qlk.Objects
                .Select(obj => new
                {
                    Object = obj,
                    SumInput = qlk.InputInfos.Where(c => c.IdObject == obj.Id).Sum(c => c.Count) ?? 0,
                    SumOutput = qlk.OutputInfos.Where(c => c.IdObject == obj.Id).Sum(c => c.Count) ?? 0
                })
                .ToList();

            int i = 0;
            return result.Select(r => new TonKho
            {
                STT = i++,
                Object = r.Object,
                Count = r.SumInput - r.SumOutput
            }).ToList();
        }
        
    }
}
