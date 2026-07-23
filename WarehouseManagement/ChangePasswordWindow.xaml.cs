using System;
using System.Windows;
using WarehouseManagement.ViewModel;

namespace WarehouseManagement
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly int _userId;
        private readonly ChangePasswordViewModel _userVM = new ChangePasswordViewModel();

        public ChangePasswordWindow(int userId, string username)
        {
            InitializeComponent();
            _userId = userId;
            txtUsername.Text = username;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtOldPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            bool success = _userVM.ChangePassword(_userId, oldPassword, newPassword, confirmPassword);
            if (success)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
