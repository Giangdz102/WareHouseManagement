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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WarehouseManagement.ViewModel;

namespace WarehouseManagement.UserControlG
{

    public partial class ControlBarUC : UserControl
    {
        public ControlBarVM ViewModel { get; set; }
        public ControlBarUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ControlBarVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.WindowState == WindowState.Maximized)
            {
                parentWindow.WindowState = WindowState.Normal;
            }
            else
            {
                parentWindow.WindowState = WindowState.Maximized;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if(parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }   
        }
    }
}
