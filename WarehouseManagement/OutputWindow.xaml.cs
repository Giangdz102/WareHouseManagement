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
    /// Interaction logic for OutputWindow.xaml
    /// </summary>
    public partial class OutputWindow : Window
    {
        OutputViewModel MV = new OutputViewModel();
        public OutputWindow()
        {
            InitializeComponent();
            LoadedData();
        }

        private void LoadedData()
        {
            LvOutput.ItemsSource = MV.GetAllOutputInfo();
            cbObject.ItemsSource = MV.GetAllObjects();
            cbCustomer.ItemsSource = MV.GetAllCustomers();
            cbStatus.ItemsSource = new List<string> { "PENDING" };
            cbStatus.SelectedValue = "PENDING";
        }

        private void LvOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvOutput.SelectedItem == null)
            {
                cbStatus.ItemsSource = new List<string> { "PENDING" };
                cbStatus.SelectedValue = "PENDING";
            }
            else
            {
                cbStatus.ItemsSource = new List<string> { "PENDING", "COMPLETED" };
                var selected = LvOutput.SelectedItem as OutputInfo;
                if (selected != null)
                {
                    cbStatus.SelectedValue = selected.Status;
                }
            }
        }

        private void cbObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvOutput.SelectedItem == null && cbObject.SelectedValue != null && cbObject.SelectedValue is int objectId)
            {
                var inputInfo = MV.GetInputInfoForObject(objectId);
                if (inputInfo != null)
                {
                    txtPriceOutput.Text = inputInfo.OutputPrice?.ToString() ?? "0";
                }
                else
                {
                    txtPriceOutput.Text = "0";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbObject.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn vật tư!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (cbCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (dpDateOutput.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Số lượng xuất phải là số nguyên dương!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int objectId = (int)cbObject.SelectedValue;
                int customerId = (int)cbCustomer.SelectedValue;

                var inputInfo = MV.GetInputInfoForObject(objectId);
                if (inputInfo == null)
                {
                    MessageBox.Show("Vật tư này chưa được nhập kho, không thể xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int stock = MV.GetStockForObject(objectId);
                if (count > stock)
                {
                    MessageBox.Show($"Số lượng tồn kho không đủ để xuất! Hiện tại còn: {stock}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double priceOutput = inputInfo.OutputPrice ?? 0;

                Output output = new Output { DateOutput = dpDateOutput.SelectedDate };
                OutputInfo outputInfo = new OutputInfo
                {
                    IdObject = objectId,
                    IdCustomer = customerId,
                    Count = count,
                    PriceOutput = priceOutput,
                    Status = cbStatus.SelectedValue as string
                };

                MV.AddOutput(output, outputInfo);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = LvOutput.SelectedItem as OutputInfo;
                if (selectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một phiếu xuất trong danh sách để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cbObject.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn vật tư!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (cbCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (dpDateOutput.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Số lượng xuất phải là số nguyên dương!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int objectId = (int)cbObject.SelectedValue;
                int customerId = (int)cbCustomer.SelectedValue;

                var inputInfo = MV.GetInputInfoForObject(objectId);
                if (inputInfo == null)
                {
                    MessageBox.Show("Vật tư này chưa được nhập kho, không thể xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int stock = MV.GetStockForObject(objectId, selectedItem.Id);
                if (count > stock)
                {
                    MessageBox.Show($"Số lượng tồn kho không đủ để xuất! Hiện tại còn: {stock}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double priceOutput = inputInfo.OutputPrice ?? 0;

                Output output = new Output { Id = selectedItem.IdOutputInfo, DateOutput = dpDateOutput.SelectedDate };
                OutputInfo outputInfo = new OutputInfo
                {
                    Id = selectedItem.Id,
                    IdObject = objectId,
                    IdCustomer = customerId,
                    IdOutputInfo = selectedItem.IdOutputInfo,
                    Count = count,
                    PriceOutput = priceOutput,
                    Status = cbStatus.SelectedValue as string
                };

                MV.UpdateOutput(output, outputInfo);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = LvOutput.SelectedItem as OutputInfo;
                if (selectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một phiếu xuất để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu xuất này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    MV.DeleteOutput(selectedItem.Id);
                    LoadedData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
