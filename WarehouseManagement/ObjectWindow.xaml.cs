using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WarehouseManagement.ViewModel;

namespace WarehouseManagement
{
    /// <summary>
    /// Interaction logic for ObjectWindow.xaml
    /// </summary>
    public partial class ObjectWindow : Window
    {
        ObjectViewModel MV = new ObjectViewModel();
        public ObjectWindow()
        {
            InitializeComponent();
            LoadedData();
        }

        private void LoadedData()
        {
            LvObject.ItemsSource = MV.GetAllObject();
            cbUnit.ItemsSource = MV.GetAllUnits();
            cbSuplier.ItemsSource = MV.GetAllSupliers();
        }
    }
}
