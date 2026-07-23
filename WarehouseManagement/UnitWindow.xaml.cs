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
using WarehouseManagement.Models;
using WarehouseManagement.ViewModel;

namespace WarehouseManagement
{
    /// <summary>
    /// Interaction logic for UnitWindow.xaml
    /// </summary>
    public partial class UnitWindow : Window
    {
        UnitViewModel MV = new UnitViewModel();
        public UnitWindow()
        {
            InitializeComponent();
            LoadedData();
        }

        private void LoadedData()
        {
            LvUnit.ItemsSource = MV.GetAllUnit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(unitName.Text))
                {
                    MessageBox.Show("Tên đơn vị không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Unit unit = new Unit
                {
                    DisplayName = unitName.Text
                };
                MV.AddUnit(unit);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Unit unit = new Unit
                {
                    Id = int.Parse(unitId.Text),
                    DisplayName = unitName.Text
                };
                MV.UpdateUnit(unit);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Unit unit = new Unit
                {
                    Id = int.Parse(unitId.Text),
                    
                };
                MV.DeleteUnit(unit);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
