using _1651_Assignment_AdvancedProgramming.Model.PersonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Controller
{
    internal class CustomerController
    {
        private static List<Customer> listCustomer = new List<Customer>();
        SQLiteConnection connection = new SQLiteConnection("Data Source=StoreManagement.db");

        public void getData()
        {
            try
            {
                connection.Open();

                var sql = "SELECT * FROM Customer";
                var cmd = new SQLiteCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer();

                    customer.Id = reader.GetInt32(0);
                    customer.Name = reader.GetString(1);
                    customer.Age = reader.GetInt32(2);
                    customer.PhoneNumber = reader.GetString(3);
                    customer.Address = reader.GetString(4);

                    listCustomer.Add(customer);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while reading data from the database:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void addCustomer()
        { 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Add Customer_");
            Console.ResetColor();
            Customer customer = new Customer();
            customer.enterInformation();
            customer.Id = listCustomer[listCustomer.Count - 1].Id + 1;

            // Add customer to List
            listCustomer.Add(customer);


            try
            {
                connection.Open();

                var sql = "INSERT INTO Customer(Name, Age, PhoneNumber, Address) " +
                    "VALUES(@name, @age, @phoneNumber, @address)";
                var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@age", customer.Age);
                cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Add Successfully");
                Console.ResetColor();
                Console.WriteLine();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("An error occurred while inserting data into the database:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void removeCustomer()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Remove Customer_");
            Console.ResetColor();
            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            bool checkRemove = false;

            foreach (var item in listCustomer)
            {
                if (item.Id == id)
                {
                    listCustomer.Remove(item);
                    checkRemove = true;
                    break;
                }
            }

            try
            {
                connection.Open();

                var sql = "DELETE FROM Customer WHERE ID = @ID";
                var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0 && checkRemove == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Customer with ID {id} has been deleted.");
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("An error occurred while deleting customer from the database:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void editCustomer()
        {
       

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Edit Customer_");
            Console.ResetColor();
            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            bool checkEdit = false;

            foreach (var item in listCustomer)
            {
                if (item.Id == id)
                {
                    checkEdit = true;
                    break;
                }
            }

            if (checkEdit == true)
            {
                Console.WriteLine("-Enter New Information-");
                Customer customer = new Customer();
                customer.enterInformation();

                try
                {
                    connection.Open();

                    var sql = "UPDATE Customer SET Name = @Name, Age = @Age, PhoneNumber = @PhoneNumber, Address = @Address WHERE ID = @ID";
                    var cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Age", customer.Age);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Customer with ID {id} has been updated.");
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
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("An error occurred while updating customer in the database:");
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Customer not found!");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public void displayAllCustomer()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_______________________________CUSTOMER_______________________________");
            Console.ResetColor();
            var id = "ID";
            var fullName = "Name";
            var age = "Age";
            var phoneNumber = "PhoneNumber";
            var address = "Address";
            Console.WriteLine($"|{id,-5:s}|{fullName,-20:s}|{age,-10:d}|{phoneNumber,-15:d}|{address,-15:d}|");

            foreach (var item in listCustomer)
            {
                Console.WriteLine($"|{item.Id,-5:d}|" +
                    $"{item.Name,-20:s}|" +
                    $"{item.Age,-10:d}|" +
                    $"{item.PhoneNumber,-15:s}|" +
                    $"{item.Address,-15:s}|");
            }
            Console.WriteLine();
        }

        public Customer getCustomerByID(int id)
        {
            Customer customer = null;

            foreach (var item in listCustomer)
            {
                if (item.Id == id)
                {
                    customer = item;
                    break;
                }
            }

            if (customer == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product Not Found!");
                Console.ResetColor();
                Console.WriteLine();
            }

            return customer;
        }
    }
}
