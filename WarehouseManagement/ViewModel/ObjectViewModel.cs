using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
