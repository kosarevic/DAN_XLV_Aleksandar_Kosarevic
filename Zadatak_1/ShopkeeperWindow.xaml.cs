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
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for ShopkeeperWindow.xaml
    /// </summary>
    public partial class ShopkeeperWindow : Window
    {
        ShopkeeperViewModel svm = new ShopkeeperViewModel();

        public ShopkeeperWindow()
        {
            InitializeComponent();
            DataContext = svm;
        }
        //Button click executes StoreProduct method.
        private void Store_Btn(object sender, RoutedEventArgs e)
        {
            svm.StoreProduct();
        }
        //Button click returns user to previous window.
        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }
    }
}
