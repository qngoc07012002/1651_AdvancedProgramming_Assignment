using _1651_Assignment_AdvancedProgramming.Model.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Utilities
{
    internal class ProductFactory
    {
        public static Product getProduct(ProductCategory productCategory)
        {
            switch (productCategory)
            {
                case ProductCategory.Food:
                    return new Food();
                case ProductCategory.Drink:
                    return new Drink();
                case ProductCategory.PersonalItem:
                    return new PersonalItem();
                default:
                    return null;
            }
        }
    }
}
