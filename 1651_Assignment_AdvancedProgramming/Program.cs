using _1651_Assignment_AdvancedProgramming.Controller;
using _1651_Assignment_AdvancedProgramming.Model.Order;
using _1651_Assignment_AdvancedProgramming.Model.PersonModel;
using System;
using System.Data.SQLite;
using System.Threading;

namespace _1651_Assignment_AdvancedProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Enter Employee
            Employee employee = new Employee();
            employee.enterInformation();

            // Create Controller
            ProductController productController = new ProductController();
            CustomerController customerController = new CustomerController();
            OrderController orderController = new OrderController();
            Order order = new Order();
            int choice = 0;

            loadingBar();
            Console.Clear();
            employee.displayInformation();
            displayMenu();


            do
            {
                Console.Write("Enter choice: ");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        customerController.displayAllCustomer();
                        break;
                    case 2:
                        customerController.addCustomer();
                        break;
                    case 3:
                        customerController.editCustomer();
                        break;
                    case 4:
                        customerController.removeCustomer();
                        break;
                    case 5:
                        productController.displayAllProduct();
                        break;
                    case 6:
                        productController.addProduct();
                        break;
                    case 7:
                        productController.editProduct();
                        break;
                    case 8:
                        productController.removeProduct();
                        break;
                    case 9:
                        order.addEmployeeInformation(employee);
                        order.createOrder();
                        break;
                    case 10:
                        orderController.displayAllOrder();
                        break;
                    case 11:
                        orderController.displayAllOrderByCustomer();
                        break;
                    case 12:
                        Console.Clear();
                        employee.displayInformation();
                        displayMenu();
                        break;
                    case 13:
                        Environment.Exit(0);
                        break;
                }
            } while (choice != 13);


            var sql = "Data Source=StoreManagement.db";
           // SQLiteConnection.CreateFile(createDB);

            SQLiteConnection conn = new SQLiteConnection(sql);
            try
            {
                conn.Open();
                //InsertData(conn, "Ngoc Test 2", 20, "084842242424", "Ha Noi");
                //UpdateData(conn, 5, "Ngoc Updated", 25, "01234567678", "Nha Trang");
                //DeleteData(conn, 6);
                SelectData(conn);
                Console.WriteLine();
                GetCustomerById(conn,1);
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

            customerController.displayAllCustomer();
        }

        public static void loadingBar()
        {
            Console.Write("Processing ");
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < 1)
            {
                Console.Write(".");
                Thread.Sleep(200);
            
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public static void displayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("_____Customer_____");
            Console.WriteLine("1.Display All Customer");
            Console.WriteLine("2.Add Customer");
            Console.WriteLine("3.Edit Customer");
            Console.WriteLine("4.Remove Customer");
            Console.WriteLine("_____Product_____");
            Console.WriteLine("5.Display All Product");
            Console.WriteLine("6.Add Product");
            Console.WriteLine("7.Edit Product");
            Console.WriteLine("8.Remove Product");
            Console.WriteLine("_____Order_____");
            Console.WriteLine("9.Create Order");
            Console.WriteLine("10.Display All Order");
            Console.WriteLine("11.Display All Order By Customer");
            Console.WriteLine("_____Setting_____");
            Console.WriteLine("12.Clear Screen");
            Console.WriteLine("13.Exit");
            Console.WriteLine();
        }

        private static void SelectData(SQLiteConnection conn)
        {
            var id = "ID";
            var fullName = "Name";
            var age = "Age";
            var phoneNumber = "PhoneNumber";
            var address = "Address";
            Console.WriteLine($"{id,-20:s}{fullName,-35:s}{age,-20:d}{phoneNumber,-20:d}{address,-20:d}");
            var sql = "SELECT * FROM Customer";
            var cmd = new SQLiteCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0),-20:d}" +
                    $"{reader.GetString(1),-35:s}" +
                    $"{reader.GetInt32(2),-20:d}" +
                     $"{reader.GetString(3),-20:s}" +
                    $"{reader.GetString(4),-20:s}");
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

        private static void GetCustomerById(SQLiteConnection conn, int customerId)
        {
            var sql = "SELECT * FROM Customer WHERE ID = @CustomerId";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var fullName = reader.GetString(1);
                    var age = reader.GetInt32(2);
                    var phoneNumber = reader.GetString(3);
                    var address = reader.GetString(4);

                    Console.WriteLine($"{id} {fullName} {age} {phoneNumber} {address}");
                }
            }

        }
    }
}
