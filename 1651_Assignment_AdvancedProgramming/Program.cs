using _1651_Assignment_AdvancedProgramming.Model.PersonModel;
using System;
using System.Data.SQLite;

namespace _1651_Assignment_AdvancedProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var sql = "Data Source=StoreManagement.db";
           // SQLiteConnection.CreateFile(createDB);

            SQLiteConnection conn = new SQLiteConnection(sql);
            try
            {
                conn.Open();
                InsertData(conn, "Ngoc Test 2", 20, "084842242424", "Ha Noi");
                //UpdateData(conn, 5, "Ngoc Updated", 25, "01234567678", "Nha Trang");
                //DeleteData(conn, 6);
                SelectData(conn);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }

            //Customer ngoc = new Customer();
            //ngoc.displayCustomer();
        }

        private static void SelectData(SQLiteConnection conn)
        {
            var id = "ID";
            var fullName = "Name";
            var age = "Age";
            var phoneNumber = "PhoneNumber";
            var address = "Address";
            Console.WriteLine($"{id,-20:s}{fullName,-35:s}{age,-20:d}{phoneNumber,10:d}{address,20:d}");
            var sql = "SELECT * FROM Customer";
            var cmd = new SQLiteCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0),-20:d}" +
                    $"{reader.GetString(1),-35:s}" +
                    $"{reader.GetInt32(2),-20:d}" +
                     $"{reader.GetString(3),10:s}" +
                    $"{reader.GetString(4),20:s}");
            }
        }

        private static void InsertData(SQLiteConnection conn, string name, int age, string phoneNumber, string address)
        {
            var sql = "INSERT INTO Customer(Name, Age, PhoneNumber, Address) " +
                "VALUES(@name, @age, @phoneNumber, @address)";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.ExecuteNonQuery();
        }

        private static void UpdateData(SQLiteConnection conn, int customerId, string newFullName, int newAge, string newPhoneNumber, string newAddress)
        {
            var sql = "UPDATE Customer SET Name = @FullName, Age = @Age, PhoneNumber = @PhoneNumber, Address = @Address WHERE ID = @CustomerId";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FullName", newFullName);
            cmd.Parameters.AddWithValue("@Age", newAge);
            cmd.Parameters.AddWithValue("@PhoneNumber", newPhoneNumber);
            cmd.Parameters.AddWithValue("@Address", newAddress);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Customer with ID {customerId} has been updated.");
            }
            else
            {
                Console.WriteLine($"Failed to update customer with ID {customerId}.");
            }
        }

        private static void DeleteData(SQLiteConnection conn, int customerId)
        {
            var sql = "DELETE FROM Customer WHERE ID = @CustomerId";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Customer with ID {customerId} has been deleted.");
            }
            else
            {
                Console.WriteLine($"Failed to delete customer with ID {customerId}.");
            }
        }
    }
}
