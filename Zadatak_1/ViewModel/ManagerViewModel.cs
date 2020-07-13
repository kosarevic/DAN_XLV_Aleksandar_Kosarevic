using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using System.Configuration;
using Zadatak_1.Actions;
using System.Windows;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Class responsible for generating data to manager window grid table
    /// </summary>
    class ManagerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }

        public ManagerViewModel()
        {
            FillList();
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
        /// Method fills the list dedicated to the coresponding window.
        /// </summary>
        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand("select * from tblProduct", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Products == null)
                    Products = new ObservableCollection<Product>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Product p = new Product
                    {
                        Id = int.Parse(row[0].ToString()),
                        Name = row[1].ToString(),
                        Code = row[2].ToString(),
                        Amount = int.Parse(row[3].ToString()),
                        Price = int.Parse(row[4].ToString()),
                        Stored = (bool)row[5]
                    };
                    Products.Add(p);
                }
            }
        }
        /// <summary>
        /// Method responsible for removing existing product from the grid.
        /// </summary>
        public void DeleteProduct()
        {
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                con.Open();
                var cmd = new SqlCommand("delete from tblProduct where ProductID = @ProductID;", con);
                cmd.Parameters.AddWithValue("@ProductID", product.Id);
                cmd.ExecuteNonQuery();
                LogActions.LogDeleteProduct(product);
                Products.Remove(product);
                con.Close();
                con.Dispose();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Delete successfull.", "Notification");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
