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
    /// Interaction logic for AddProductWidnow.xaml
    /// </summary>
    public partial class AddProductWidnow : Window
    {
        AddProductViewModel apvm = new AddProductViewModel();

        public AddProductWidnow()
        {
            InitializeComponent();
            DataContext = apvm;
            Name_TBox.Text = "";
            Code_TBox.Text = "";
            Amount_TBox.Text = "";
            Price_TBox.Text = "";
        }

        private void Btn_Confirm(object sender, RoutedEventArgs e)
        {
            apvm.AddProduct();
            ManagerWindow window = new ManagerWindow();
            window.Show();
            this.Close();
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            ManagerWindow window = new ManagerWindow();
            window.Show();
            this.Close();
        }
    }
}
