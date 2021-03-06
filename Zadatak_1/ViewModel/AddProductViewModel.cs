﻿using System;
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
    /// Class responsible for creating new product, depending on inserted values.
    /// </summary>
    public class AddProductViewModel : INotifyPropertyChanged
    {
        public AddProductViewModel()
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
        /// Method responsible for adding new product based on inserted values.
        /// </summary>
        public void AddProduct()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"insert into tblProduct values (@Name, @Code, @Amount, @Price, @Stored);", conn);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Code", product.Code);
                cmd.Parameters.AddWithValue("@Amount", product.Amount);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Stored", false);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LogActions.LogCreateProduct(product);
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Product Successfully created.", "Notification");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
