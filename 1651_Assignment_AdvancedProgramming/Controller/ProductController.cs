using _1651_Assignment_AdvancedProgramming.Model.ProductModel;
using _1651_Assignment_AdvancedProgramming.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Controller
{
    internal class ProductController
    {
        private List<Product> listProduct;
        SQLiteConnection connection = new SQLiteConnection("Data Source=StoreManagement.db");

        public void addProduct()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Add Product_");
            Console.ResetColor();
            Console.WriteLine("Category");
            Console.WriteLine("1.Food");
            Console.WriteLine("2.Drink");
            Console.WriteLine("3.Personal Item");
            Console.Write("Enter Category: ");
            int category = int.Parse(Console.ReadLine());


            Product product = null;
            switch (category)
            {
                case 1:
                    product = ProductFactory.getProduct(ProductCategory.Food);
                    break;
                case 2:
                    product = ProductFactory.getProduct(ProductCategory.Drink);
                    break;
                case 3:
                    product = ProductFactory.getProduct(ProductCategory.PersonalItem);
                    break;
            }

            product.Category = product.getCategory();

            Console.Write("Name: ");
            product.Name = Console.ReadLine();
            Console.Write("Price: ");
            product.Price = double.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            product.Quantity = int.Parse(Console.ReadLine());


            connection.Open();

            var sql = "INSERT INTO Product(Name, Price, Quantity, Category) " +
                "VALUES(@name, @price, @quantity, @category)";
            var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@category", product.Category);
            cmd.ExecuteNonQuery();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Add Successfully");
            Console.ResetColor();
            Console.WriteLine();

            connection.Close();
        }

        public void removeProduct()
        {
            connection.Open();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Remove Product_");
            Console.ResetColor();
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            var sql = "DELETE FROM Product WHERE ID = @ID";
            var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ID", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Product with ID {id} has been deleted.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to delete customer with ID {id}.");
                Console.ResetColor();
            }
            Console.WriteLine();

            connection.Close();
        }

        public void editProduct()
        {
            connection.Open();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Edit Product_");
            Console.ResetColor();
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("-Enter New Information-");
            Console.WriteLine("Category");
            Console.WriteLine("1.Food");
            Console.WriteLine("2.Drink");
            Console.WriteLine("3.Personal Item");
            Console.Write("Enter Category: ");
            int category = int.Parse(Console.ReadLine());


            Product product = null;
            switch (category)
            {
                case 1:
                    product = ProductFactory.getProduct(ProductCategory.Food);
                    break;
                case 2:
                    product = ProductFactory.getProduct(ProductCategory.Drink);
                    break;
                case 3:
                    product = ProductFactory.getProduct(ProductCategory.PersonalItem);
                    break;
            }

            product.Category = product.getCategory();

            Console.Write("Name: ");
            product.Name = Console.ReadLine();
            Console.Write("Price: ");
            product.Price = double.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            product.Quantity = int.Parse(Console.ReadLine());

            var sql = "UPDATE Product SET Name = @Name, Price = @Price, Quantity = @Quantity, Category = @Category WHERE ID = @ID";
            var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            cmd.Parameters.AddWithValue("@ID", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Product with ID {id} has been updated.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to update customer with ID {id}.");
                Console.ResetColor();
            }
            Console.WriteLine();

            connection.Close();
        }

        public void displayAllProduct()
        {
            connection.Open();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("________________________________Product_______________________________");
            Console.ResetColor();
            var id = "ID";
            var name = "Name";
            var price = "Price($)";
            var quantity = "Quantity";
            var category = "Category";
            Console.WriteLine($"|{id,-5:s}|{name,-20:s}|{price,-10:d}|{quantity,-15:d}|{category,-15:d}|");
            var sql = "SELECT * FROM Product";
            var cmd = new SQLiteCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"|{reader.GetInt32(0),-5:d}|" +
                    $"{reader.GetString(1),-20:s}|" +
                    $"{reader.GetDouble(2),-10:g}|" +
                     $"{reader.GetInt32(3),-15:d}|" +
                    $"{reader.GetString(4),-15:s}|");
            }
            Console.WriteLine();

            connection.Close();
        }

        public Product getProductByID(int id)
        {
            connection.Open();

            var sql = "SELECT * FROM Product WHERE ID = @ProductId";
            var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProductId", id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string category = reader.GetString(4);
                    Product product = null;

                    switch (category)
                    {
                        case "Food":
                            product = ProductFactory.getProduct(ProductCategory.Food);
                            break;
                        case "Drink":
                            product = ProductFactory.getProduct(ProductCategory.Drink);
                            break;
                        case "PersonalItem":
                            product = ProductFactory.getProduct(ProductCategory.PersonalItem);
                            break;
                    }

                    product.Id = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetDouble(2);
                    product.Quantity = reader.GetInt32(3);
                    product.Category = reader.GetString(4);

                    connection.Close();
                    return product;
                }
                else
                {
                    connection.Close();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Product Not Found!");
                    Console.ResetColor();
                    return null;
                }
            }
        }

        public Product getProductByName(string name)
        {
            return null;
        }
    }
}
