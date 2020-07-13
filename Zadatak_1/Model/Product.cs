using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public bool Stored { get; set; }

        public Product()
        {
        }

        public Product(int id, string name, string code, int amount, int price, bool stored)
        {
            Id = id;
            Name = name;
            Code = code;
            Amount = amount;
            Price = price;
            Stored = stored;
        }
    }
}
