using _1651_Assignment_AdvancedProgramming.Model.Order;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Controller
{
    internal class OrderController
    {
        private List<Order> listOrder = new List<Order>();
        SQLiteConnection connection = new SQLiteConnection("Data Source=StoreManagement.db");

        public void addOrder(Order order)
        {
            try
            {
                connection.Open();

                var sql = "INSERT INTO [Order] (CustomerID, Employee, PaymentMethod, TotalPrice, Date) " +
                    "VALUES (@customerID, @employee, @paymentMethod, @totalPrice, @date)";
                var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@customerID", order.Customer.Id);
                cmd.Parameters.AddWithValue("@employee", order.Employee.Name);
                cmd.Parameters.AddWithValue("@paymentMethod", order.PaymentMethod.GetType().Name);
                cmd.Parameters.AddWithValue("@totalPrice", order.TotalPrice);
                cmd.Parameters.AddWithValue("@date", order.Date.ToString("dd/MM/yyyy"));
                cmd.ExecuteNonQuery();

                // Lấy ID của đơn hàng vừa thêm
                var orderID = connection.LastInsertRowId;

                foreach (var orderItem in order.OrderItemList)
                {
                    sql = "INSERT INTO OrderProduct (OrderID, ProductID, Quantity, TotalPrice) " +
                        "VALUES (@orderID, @productID, @quantity, @totalPrice)";
                    cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@orderID", orderID);
                    cmd.Parameters.AddWithValue("@productID", orderItem.Id);
                    cmd.Parameters.AddWithValue("@quantity", orderItem.Quantity);
                    cmd.Parameters.AddWithValue("@totalPrice", orderItem.Price);
                    cmd.ExecuteNonQuery();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Add Order Successfully");
                Console.ResetColor();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred while adding the order: " + ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

        public void displayAllOrder()
        {
            try
            {
                connection.Open();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("_______________________________________________ORDER________________________________________________");
                Console.ResetColor();
                var orderID = "ID";
                var customerID = "CustomerID";
                var employee = "Employee";
                var paymentMethod = "Payment Method";
                var totalPrice = "Total Price";
                var date = "Date";
                Console.WriteLine($"|{orderID,-10:s}|{customerID,-12:s}|{employee,-15:s}|{paymentMethod,-30:s}|{totalPrice,-12:s}|{date,-15:s}|");
                var sql = "SELECT * FROM [Order]";
                var cmd = new SQLiteCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int orderIDValue = reader.GetInt32(0);
                    int customerIDValue = reader.GetInt32(1);
                    string employeeValue = reader.GetString(2);
                    string paymentMethodValue = reader.GetString(3);
                    double totalPriceValue = reader.GetDouble(4);
                    string dateTime = reader.GetString(5);

                    Console.WriteLine($"|{orderIDValue,-10:d}|{customerIDValue,-12:d}|{employeeValue,-15:s}|{paymentMethodValue,-30:s}|{totalPriceValue,-12:f2}|{dateTime,-15:s}|");
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred while displaying orders: " + ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }

        public void displayAllOrderByCustomer()
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                int customerID = int.Parse(Console.ReadLine());

                connection.Open();

                var sql = "SELECT * FROM [Order] WHERE CustomerID = @customerID";
                var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@customerID", customerID);
                var reader = cmd.ExecuteReader();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"___________________________________ORDER CUSTOMER {customerID}____________________________________");
                Console.ResetColor();
                var orderID = "ID";
                var employee = "Employee";
                var paymentMethod = "Payment Method";
                var totalPrice = "Total Price";
                var date = "Date";
                Console.WriteLine($"|{orderID,-10:s}|{employee,-15:s}|{paymentMethod,-30:s}|{totalPrice,-12:s}|{date,-15:s}|");

                while (reader.Read())
                {
                    int orderIDValue = reader.GetInt32(0);
                    string employeeValue = reader.GetString(2);
                    string paymentMethodValue = reader.GetString(3);
                    double totalPriceValue = reader.GetDouble(4);
                    string dateTime = reader.GetString(5);

                    Console.WriteLine($"|{orderIDValue,-10:d}|{employeeValue,-15:s}|{paymentMethodValue,-30:s}|{totalPriceValue,-12:f2}|{dateTime,-15:s}|");
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred while displaying orders: " + ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
