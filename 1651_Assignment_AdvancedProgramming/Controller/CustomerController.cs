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

            connection.Close();
        }

        public void removeCustomer()
        {
            connection.Open();

            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            var sql = "DELETE FROM Customer WHERE ID = @ID";
            var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ID", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Customer with ID {id} has been deleted.");
            }
            else
            {
                Console.WriteLine($"Failed to delete customer with ID {id}.");
            }

            connection.Close();
        }

        public void editCustomer()
        {
            connection.Open();

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
            cmd.Parameters.AddWithValue("@ID", customer.Id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Customer with ID {id} has been updated.");
            }
            else
            {
                Console.WriteLine($"Failed to update customer with ID {id}.");
            }

            connection.Close();
        }

        public void displayAllCustomer()
        {
            connection.Open();

            var id = "ID";
            var fullName = "Name";
            var age = "Age";
            var phoneNumber = "PhoneNumber";
            var address = "Address";
            Console.WriteLine($"{id,-5:s}{fullName,-35:s}{age,-10:d}{phoneNumber,-15:d}{address,-15:d}");
            var sql = "SELECT * FROM Customer";
            var cmd = new SQLiteCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0),-5:d}" +
                    $"{reader.GetString(1),-35:s}" +
                    $"{reader.GetInt32(2),-10:d}" +
                     $"{reader.GetString(3),-15:s}" +
                    $"{reader.GetString(4),-15:s}");
            }

            connection.Close();
        }

        public Customer getCustomerByID(int id)
        {
            return null;
        }
    }
}
