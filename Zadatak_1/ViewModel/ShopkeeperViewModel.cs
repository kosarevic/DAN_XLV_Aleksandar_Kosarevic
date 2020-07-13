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
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
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
                    if (p.Stored) RemainingSpace -= p.Amount;
                }
            }
        }

        public void StoreProduct()
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            con.Open();
            var cmd = new SqlCommand("update tblProduct set Stored=@Stored where ProductID=@ProductID;", con);
            cmd.Parameters.AddWithValue("@Stored", true);
            cmd.Parameters.AddWithValue("@ProductID", Product.Id);
            cmd.ExecuteNonQuery();
            Products.Clear();
            FillList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
