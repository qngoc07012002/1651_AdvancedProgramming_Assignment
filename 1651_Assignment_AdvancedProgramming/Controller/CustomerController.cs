using _1651_Assignment_AdvancedProgramming.Model.PersonModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Controller
{
    internal class CustomerController
    {
        private List<Customer> listCustomer;
        SQLiteConnection connection = new SQLiteConnection("Data Source=StoreManagement.db");

        public void addCustomer()
        {
            connection.Open();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Add Customer_");
            Console.ResetColor();
            Customer customer = new Customer();
            customer.enterInformation();

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

        public void removeCustomer()
        {
            connection.Open();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Remove Customer_");
            Console.ResetColor();
            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            var sql = "DELETE FROM Customer WHERE ID = @ID";
            var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ID", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
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

        public void editCustomer()
        {
            connection.Open();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Edit Customer_");
            Console.ResetColor();
            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("-Enter New Information-");
            Customer customer = new Customer();
            customer.enterInformation();

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

        public void displayAllCustomer()
        {
            connection.Open();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_______________________________CUSTOMER_______________________________");
            Console.ResetColor();
            var id = "ID";
            var fullName = "Name";
            var age = "Age";
            var phoneNumber = "PhoneNumber";
            var address = "Address";
            Console.WriteLine($"|{id,-5:s}|{fullName,-20:s}|{age,-10:d}|{phoneNumber,-15:d}|{address,-15:d}|");
            var sql = "SELECT * FROM Customer";
            var cmd = new SQLiteCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"|{reader.GetInt32(0),-5:d}|" +
                    $"{reader.GetString(1),-20:s}|" +
                    $"{reader.GetInt32(2),-10:d}|" +
                     $"{reader.GetString(3),-15:s}|" +
                    $"{reader.GetString(4),-15:s}|");
            }
            Console.WriteLine();

            connection.Close();
        }

        public Customer getCustomerByID(int id)
        {
            connection.Open();

            var sql = "SELECT * FROM Customer WHERE ID = @CustomerId";
            var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@CustomerId", id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Customer customer = new Customer();

                    customer.Id = reader.GetInt32(0);
                    customer.Name = reader.GetString(1);
                    customer.Age = reader.GetInt32(2);
                    customer.PhoneNumber = reader.GetString(3);
                    customer.Address = reader.GetString(4);

                    connection.Close();
                    return customer;
                }
                else
                {
                    connection.Close();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Customer Not Found!");
                    Console.ResetColor();
                    return null;
                }
            }

            
        }
    }
}
