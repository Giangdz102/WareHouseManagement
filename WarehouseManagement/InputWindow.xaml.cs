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
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        InputViewModel MV = new InputViewModel();
        public InputWindow()
        {
            InitializeComponent();
            LoadedData();
        }

        private void LoadedData()
        {
            LvInput.ItemsSource = MV.GetAllInputInfo();
            cbObject.ItemsSource = MV.GetAllObjects();
            cbStatus.ItemsSource = new List<string> { "PENDING" };
            cbStatus.SelectedValue = "PENDING";
        }

        private void LvInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvInput.SelectedItem == null)
            {
                cbStatus.ItemsSource = new List<string> { "PENDING" };
                cbStatus.SelectedValue = "PENDING";
            }
            else
            {
                cbStatus.ItemsSource = new List<string> { "PENDING", "COMPLETED" };
                var selected = LvInput.SelectedItem as InputInfo;
                if (selected != null)
                {
                    cbStatus.SelectedValue = selected.Status;
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
                if (dpDateInput.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày nhập!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Số lượng nhập phải là số nguyên dương!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!double.TryParse(txtPriceInput.Text, out double priceInput) || priceInput < 0)
                {
                    MessageBox.Show("Giá nhập phải là số thực không âm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!double.TryParse(txtPriceOutput.Text, out double priceOutput) || priceOutput < 0)
                {
                    MessageBox.Show("Giá xuất phải là số thực không âm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int objectId = (int)cbObject.SelectedValue;

                Input input = new Input { DateInput = dpDateInput.SelectedDate };
                InputInfo inputInfo = new InputInfo
                {
                    IdObject = objectId,
                    Count = count,
                    InputPrice = priceInput,
                    OutputPrice = priceOutput,
                    Status = cbStatus.SelectedValue as string
                };

                MV.AddInput(input, inputInfo);
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
                var selectedItem = LvInput.SelectedItem as InputInfo;
                if (selectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một phiếu nhập trong danh sách để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cbObject.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn vật tư!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (dpDateInput.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày nhập!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Số lượng nhập phải là số nguyên dương!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!double.TryParse(txtPriceInput.Text, out double priceInput) || priceInput < 0)
                {
                    MessageBox.Show("Giá nhập phải là số thực không âm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!double.TryParse(txtPriceOutput.Text, out double priceOutput) || priceOutput < 0)
                {
                    MessageBox.Show("Giá xuất phải là số thực không âm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int objectId = (int)cbObject.SelectedValue;

                Input input = new Input { Id = selectedItem.IdInput, DateInput = dpDateInput.SelectedDate };
                InputInfo inputInfo = new InputInfo
                {
                    Id = selectedItem.Id,
                    IdObject = objectId,
                    IdInput = selectedItem.IdInput,
                    Count = count,
                    InputPrice = priceInput,
                    OutputPrice = priceOutput,
                    Status = cbStatus.SelectedValue as string
                };

                MV.UpdateInput(input, inputInfo);
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
                var selectedItem = LvInput.SelectedItem as InputInfo;
                if (selectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn một phiếu nhập để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu nhập này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    MV.DeleteInput(selectedItem.Id);
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
