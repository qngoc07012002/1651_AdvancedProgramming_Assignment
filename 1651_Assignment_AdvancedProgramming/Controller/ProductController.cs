using _1651_Assignment_AdvancedProgramming.Model.ProductModel;
using _1651_Assignment_AdvancedProgramming.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Controller
{
    internal class ProductController
    {
        public List<Product> listProduct = new List<Product>();
        SQLiteConnection connection = new SQLiteConnection("Data Source=StoreManagement.db");

        public void getData()
        {
            try
            {
                connection.Open();

                var sql = "SELECT * FROM Product";
                var cmd = new SQLiteCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = null;

                    string category = reader.GetString(4);

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
                    product.Category = category;

                    listProduct.Add(product);


                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred while getting products:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

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
            product.Id = listProduct[listProduct.Count - 1].Id + 1;


            // Add Product to List
            listProduct.Add(product);

            // Insert Product to Database
            try
            {
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
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred while adding the product:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

        public void removeProduct()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Remove Product_");
            Console.ResetColor();
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            bool checkRemove = false;

            foreach (var item in listProduct)
            {
                if (item.Id == id)
                {
                    listProduct.Remove(item);
                    checkRemove = true;
                    break;
                }
            }

            try
            {
                connection.Open();

                var sql = "DELETE FROM Product WHERE ID = @ID";
                var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0 && checkRemove == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Product with ID {id} has been deleted.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to delete product with ID {id}.");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred while removing the product:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

        public void editProduct()
        {
           
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Edit Product_");
            Console.ResetColor();
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            bool checkEdit = false;

            foreach (var item in listProduct)
            {
                if (item.Id == id)
                {
                    checkEdit = true;
                    break;
                }
            }

            int category = 0;
            Product product = null;

            if (checkEdit == true)
            {
                Console.WriteLine("-Enter New Information-");
                Console.WriteLine("Category");
                Console.WriteLine("1.Food");
                Console.WriteLine("2.Drink");
                Console.WriteLine("3.Personal Item");
                Console.Write("Enter Category: ");
                category = int.Parse(Console.ReadLine());

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

                try
                {
                    connection.Open();

                    var sql = "UPDATE Product SET Name = @Name, Price = @Price, Quantity = @Quantity, Category = @Category WHERE ID = @ID";
                    var cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0 && checkEdit == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Product with ID {id} has been updated.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Failed to update product with ID {id}.");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error occurred while updating the product:");
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine();
                }
                finally
                {
                    connection.Close();
                }
            } else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Product not found!");
                Console.ResetColor();
                Console.WriteLine();
            }


            
        }

        public void displayAllProduct()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("________________________________Product_______________________________");
            Console.ResetColor();
            var id = "ID";
            var name = "Name";
            var price = "Price($)";
            var quantity = "Quantity";
            var category = "Category";
            Console.WriteLine($"|{id,-5:s}|{name,-20:s}|{price,-10:d}|{quantity,-15:d}|{category,-15:d}|");

            foreach (var item in listProduct)
            {
                Console.WriteLine($"|{item.Id,-5:d}|" +
                     $"{item.Name,-20:s}|" +
                     $"{item.Price,-10:g}|" +
                     $"{item.Quantity,-15:d}|" +
                     $"{item.Category,-15:s}|");
            }
            Console.WriteLine();

        }

        public Product getProductByID(int id)
        {
            Product product = null;

            foreach (var item in listProduct)
            {
                if (item.Id == id)
                {
                    product = item;
                    break;
                }
            }

            if (product == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product Not Found!");
                Console.ResetColor();
                Console.WriteLine();
            } 

            return product;
        }

        public void updateQuantityById(int id, int quantity)
        {
            foreach (var item in listProduct)
            {
                if (item.Id == id)
                {
                    item.Quantity = quantity;
                    break;
                }
            }

            try
            {
                connection.Open();
                var sql = "UPDATE Product SET Quantity = @Quantity WHERE ID = @ID";
                var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@ID", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ResetColor();
                Console.WriteLine();
            }
            
        }
    }
}
