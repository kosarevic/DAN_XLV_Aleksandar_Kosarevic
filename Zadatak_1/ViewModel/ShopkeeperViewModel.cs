using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Class responsible for generating data to shopkeeper window grid table
    /// </summary>
    class ShopkeeperViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }

        public ShopkeeperViewModel()
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
        //Variable added for purpose of calculating remaining space in warehouse.
        private int remainingSpace;

        public int RemainingSpace
        {
            get { return remainingSpace; }
            set
            {
                if (remainingSpace != value)
                {
                    remainingSpace = value;
                    OnPropertyChanged("RemainingSpace");
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

                RemainingSpace = 100;

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
                    //Remaining space is re-calculated on each storing procedure.
                    if (p.Stored) RemainingSpace -= p.Amount;
                }
            }
        }
        /// <summary>
        /// Method executes query for storing desired product in warehouse.
        /// </summary>
        public void StoreProduct()
        {
            //Condition checks if product is alredy stored in warehouse.
            if (!Product.Stored)
            {
                int ProductId = Product.Id;
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                con.Open();
                var cmd = new SqlCommand("update tblProduct set Stored=@Stored where ProductID=@ProductID;", con);
                cmd.Parameters.AddWithValue("@Stored", true);
                cmd.Parameters.AddWithValue("@ProductID", Product.Id);
                cmd.ExecuteNonQuery();
                Products.Clear();
                FillList();
                if (RemainingSpace >= 0)
                {
                    StoreSuccessfull();
                }
                //If avalaible space is less than 0, reverse query is executed to reload previous one.
                else
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                    con.Open();
                    cmd = new SqlCommand("update tblProduct set Stored=@Stored where ProductID=@ProductID;", con);
                    cmd.Parameters.AddWithValue("@Stored", false);
                    cmd.Parameters.AddWithValue("@ProductID", ProductId);
                    cmd.ExecuteNonQuery();
                    Products.Clear();
                    FillList();
                    NoRemainingSpace();
                }
            }
            else
            {
                ProductAlreadyStored();
            }
        }
        /// <summary>
        /// Delegates for various conditions, informing user about success or failure of desired actions.
        /// </summary>
        public delegate void Notification();

        public event Notification OnNotification;

        public void StoreSuccessfull()
        {
            OnNotification = () =>
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Product successfully stored.", "Notification");
            };
            OnNotification.Invoke();
        }

        public void ProductAlreadyStored()
        {
            OnNotification = () =>
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Product did not store. Reason: Product is already stored.", "Notification");
            };
            OnNotification.Invoke();
        }

        public void NoRemainingSpace()
        {
            OnNotification = () =>
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Product did not store. Reason: Missing available space.", "Notification");
            };
            OnNotification.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
