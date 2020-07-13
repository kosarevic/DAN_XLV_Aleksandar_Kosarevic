using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Actions;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Class responsible for generating data to edit window page.
    /// </summary>
    class EditProductViewModel : INotifyPropertyChanged
    {
        public EditProductViewModel()
        {
            Product = new Product();
        }

        private Product product;

        public Product Product
        {
            get { return product; }
            set
            {
                if (product != value)
                {
                    product = value;
                    OnPropertyChanged("Product");
                }
            }
        }
        /// <summary>
        /// Method executes query for editing selected product in the database.
        /// </summary>
        public void EditProduct()
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            con.Open();
            var cmd = new SqlCommand("update tblProduct set Name=@Name, Code=@Code, Amount=@Amount, Price=@Price, Stored=@Stored where ProductID=@ProductID;", con);
            cmd.Parameters.AddWithValue("@ProductID", product.Id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Code", product.Code);
            cmd.Parameters.AddWithValue("@Amount", product.Amount);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Stored", EditProductWindow.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            LogActions.LogEditProduct(product);
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Product successfully edited.", "Notification");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
