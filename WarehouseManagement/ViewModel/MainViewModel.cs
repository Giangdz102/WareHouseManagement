using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WarehouseManagement.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsChecked=false;
        public MainViewModel()
        {
            
            if (!IsChecked)
            {
                IsChecked = true;
                // Initialize your ViewModel properties and commands here
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
        }
    }
}
