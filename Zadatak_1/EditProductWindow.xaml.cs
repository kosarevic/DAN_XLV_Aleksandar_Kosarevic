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
using Zadatak_1.Model;
using Zadatak_1.Validation;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        EditProductViewModel epvm = new EditProductViewModel();

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            epvm.Product = product;
            DataContext = epvm;
        }

        private void Btn_Confirm(object sender, RoutedEventArgs e)
        {
            if (ValidateProduct.Validate(epvm.Product))
            {
                epvm.EditProduct();
                ManagerWindow window = new ManagerWindow();
                window.Show();
                this.Close(); 
            }
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            ManagerWindow window = new ManagerWindow();
            window.Show();
            this.Close();
        }
    }
}
