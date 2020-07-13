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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        ManagerViewModel mvm = new ManagerViewModel();

        public ManagerWindow()
        {
            InitializeComponent();
            DataContext = mvm;
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }

        private void AddProduct_Button(object sender, RoutedEventArgs e)
        {
            AddProductWidnow window = new AddProductWidnow();
            window.Show();
            this.Close();
        }

        private void HyperlinkButton_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                mvm.DeleteProduct();
                messageBoxResult = System.Windows.MessageBox.Show("Delete Successfull", "Notification");
            }
        }

        private void HyperlinkButton_Edit(object sender, RoutedEventArgs e)
        {
            EditProductWindow EditPage = new EditProductWindow(mvm.Product);
            EditPage.Show();
            this.Close();
        }
    }
}
