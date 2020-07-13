using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Validation
{
    /// <summary>
    /// Class responsible for validating Product creation and editing.
    /// </summary>
    public static class ValidateProduct
    {

        public static bool Validate(Product p)
        {
            if (p.Name == null || p.Name == "")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Incorrect product name, please try again.", "Notification");
                return false;
            }
            else if (p.Code == null || p.Code == "")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Incorrect product code, please try again.", "Notification");
                return false;
            }
            else if (p.Amount < 1)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Product amount cannot be less than 1, please try again.", "Notification");
                return false;
            }
            else if (p.Price < 1)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Product price cannot be less than 1, please try again.", "Notification");
                return false;
            }

            return true;
        }

    }
}
