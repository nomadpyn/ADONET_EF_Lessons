using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l5_LINQ_start
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public Product()
        {
            this.Name = null;
            this.Price= 0;
            this.Count = 0;
        }
        public Product(string name, int price, int count)
        {
            this.Name = name;
            this.Price = price;
            this.Count = count;
        }

        public override string ToString()
        {
            return $"{this.Name}. Цена {this.Price} руб.\nКоличество на складе {this.Count} шт.";
        }
    }
}
