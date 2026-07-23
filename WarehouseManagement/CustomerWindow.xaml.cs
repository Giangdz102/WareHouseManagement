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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        CustomerViewModel MV = new CustomerViewModel();
        public CustomerWindow()
        {
            InitializeComponent();
            LoadedData();
        }

        private void LoadedData()
        {
            LvCustomer.ItemsSource = MV.GetAllCustomer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomerName.Text) || string.IsNullOrEmpty(txtCustomerAddress.Text) || string.IsNullOrEmpty(txtCustomerPhone.Text) || string.IsNullOrEmpty(txtCustomerEmail.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng", "Lỗi thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var name = txtCustomerName.Text;
                var address = txtCustomerAddress.Text;
                var email = txtCustomerEmail.Text;
                if(email.IndexOf("@gmail.com") == -1)
                {
                    MessageBox.Show("Email không hợp lệ", "Lỗi email", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var phone = txtCustomerPhone.Text.Trim();
                if (phone.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải có 10 số",
                                    "Lỗi số điện thoại", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var moreInfo = txtCustomerMoreInfo.Text;
                var date = txtCustomerDate.Text;
                date = string.IsNullOrEmpty(date) ? DateTime.Now.ToString("yyyy-MM-dd") : date;

                Customer customer = new Customer
                {
                    DisplayName = name,
                    Address = address,
                    Email = email,
                    Phone = phone,
                    MoreInfo = moreInfo,
                    ContractDate = DateTime.Parse(date)
                };
                MV.AddCustomer(customer);
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
                Customer customer = new Customer
                {
                    Id = int.Parse(txtCustomerId.Text),
                    DisplayName = txtCustomerName.Text,
                    Address = txtCustomerAddress.Text,
                    Email = txtCustomerEmail.Text,
                    Phone = txtCustomerPhone.Text,
                    MoreInfo = txtCustomerMoreInfo.Text,
                    ContractDate = DateTime.Parse(txtCustomerDate.Text)
                };
                MV.UpdateCustomer(customer);
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
                Customer customer = new Customer
                {
                    Id = int.Parse(txtCustomerId.Text),
                    
                };
                MV.DeleteCustomer(customer);
                LoadedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
