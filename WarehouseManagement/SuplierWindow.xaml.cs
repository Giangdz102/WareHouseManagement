using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for SuplierWindow.xaml
    /// </summary>
    public partial class SuplierWindow : Window
    {
        SuplierViewModel MV = new SuplierViewModel();
        public SuplierWindow()
        {
            InitializeComponent();
            LoadedData();
        }

        private void LoadedData()
        {
            LvSuplier.ItemsSource = MV.GetAllSuplier();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSuplierName.Text) || string.IsNullOrEmpty(txtSuplierAddress.Text) || string.IsNullOrEmpty(txtSuplierPhone.Text) || string.IsNullOrEmpty(txtSuplierEmail.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng", "Lỗi thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var name = txtSuplierName.Text;
                var address = txtSuplierAddress.Text;
                var email = txtSuplierEmail.Text;
                if (email.IndexOf("@gmail.com") == -1)
                {
                    MessageBox.Show("Email không hợp lệ", "Lỗi email", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var phone = txtSuplierPhone.Text.Trim();
                if (phone.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải có 10 số",
                                    "Lỗi số điện thoại", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var moreInfo = txtSuplierMoreInfo.Text;
                var date = txtSuplierDate.Text;
                date = string.IsNullOrEmpty(date) ? DateTime.Now.ToString("yyyy-MM-dd") : date;

                Suplier suplier = new Suplier
                {
                    DisplayName = name,
                    Address = address,
                    Email = email,
                    Phone = phone,
                    MoreInfo = moreInfo,
                    ContractDate = DateTime.Parse(date)
                }
                ;
                MV.AddSuplier(suplier);
                LoadedData();
                MessageBox.Show("Thêm khách hàng thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

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
                Suplier suplier = new Suplier
                {
                    Id = int.Parse(txtSuplierId.Text),
                    DisplayName = txtSuplierName.Text,
                    Address = txtSuplierAddress.Text,
                    Email = txtSuplierEmail.Text,
                    Phone = txtSuplierPhone.Text,
                    MoreInfo = txtSuplierMoreInfo.Text,
                    ContractDate = DateTime.Parse(txtSuplierDate.Text)
                };
                MV.UpdateSuplier(suplier);
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
                Suplier suplier = new Suplier
                {
                    Id = int.Parse(txtSuplierId.Text),
                   
                };
                MV.DeleteSuplier(suplier);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
