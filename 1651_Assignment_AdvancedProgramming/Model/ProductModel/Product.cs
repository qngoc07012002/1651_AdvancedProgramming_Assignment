using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.ProductModel
{
    internal abstract class Product
    {
        private int id;
        private string name;
        private double price;
        private int quantity;
        private string category;

        public int Id { get { return id; } set { id = value; } }    
        public string Name { get { return name; } set { name = value; } }
        public double Price { get { return price; } set { price = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }

        public string Category { get { return category; } set { category = value; } }

        public abstract string getCategory();
    }
}
