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

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //If username is as value below, Employe window is engaged.
            if (txtUsername.Text == "Man2019" && txtPassword.Password == "Man2019")
            {
                ManagerWindow window = new ManagerWindow();
                window.Show();
                this.Close();
            }
            //If username is as value below, User window is engaged.
            else if (txtUsername.Text == "Mag2019" && txtPassword.Password == "Mag2019")
            {
                ShopkeeperWindow window = new ShopkeeperWindow();
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect.");
            }
        }
    }
}
