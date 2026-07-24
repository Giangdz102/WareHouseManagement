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
using WarehouseManagement.Models;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbUnit.SelectedValue == null || string.IsNullOrWhiteSpace(txtObjectName.Text) || cbSuplier.SelectedValue == null)
                {
                    MessageBox.Show("Không được để trống tên vật tư, đơn vị đo, và nhà cung cấp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Models.Object objects = new Models.Object()
                {
                    
                    DisplayName = txtObjectName.Text,
                    IdUnit = (int)cbUnit.SelectedValue,
                    IdSuplier = (int)cbSuplier.SelectedValue,
                    Qrcode = txtQRCode.Text,
                    BarCode = txtBarCode.Text
                };
                MV.AddObject(objects);
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
                var selectedItem = LvObject.SelectedItem as Models.Object;
                if (selectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cbUnit.SelectedValue == null || string.IsNullOrWhiteSpace(txtObjectName.Text) || cbSuplier.SelectedValue == null)
                {
                    MessageBox.Show("Không được để trống tên vật tư, đơn vị đo, và nhà cung cấp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Models.Object objects = new Models.Object()
                {
                    Id = selectedItem.Id,
                    DisplayName = txtObjectName.Text,
                    IdUnit = (int)cbUnit.SelectedValue,
                    IdSuplier = (int)cbSuplier.SelectedValue,
                    Qrcode = txtQRCode.Text,
                    BarCode = txtBarCode.Text
                };
                MV.UpdateObject(objects);
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
                var selectedItem = LvObject.SelectedItem as Models.Object;
                if (selectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư '{selectedItem.DisplayName}' không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MV.DeleteObject(selectedItem);
                    LoadedData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
