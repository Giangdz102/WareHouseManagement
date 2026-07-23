using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WarehouseManagement.Models;
using WarehouseManagement.ViewModel;

namespace WarehouseManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //MainViewModel MV = new MainViewModel();
        private User? currentUser;

        public MainWindow()
        {
            InitializeComponent();
            TonKhoData();
        }

        public MainWindow(User user) : this()
        {
            currentUser = user;
            CheckUserRole();
        }

        private void CheckUserRole()
        {
            if (currentUser == null || currentUser.IdRole != 1)
            {
                btnNguoiDung.Visibility = Visibility.Collapsed;
            }
        }

        private void TonKhoData()
        {
            MainViewModel MV = new MainViewModel();
            LvTonKho.ItemsSource = MV.TonKhoData();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UnitWindow unitWindow = new UnitWindow();
            unitWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SuplierWindow suplierWindow = new SuplierWindow();
            suplierWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ObjectWindow objectWindow = new ObjectWindow();
            objectWindow.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            userWindow.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            InputWindow inputWindow = new InputWindow();
            inputWindow.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            OutputWindow outputWindow = new OutputWindow();
            outputWindow.ShowDialog();
        }
    }
}