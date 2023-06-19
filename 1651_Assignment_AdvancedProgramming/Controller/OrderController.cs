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
        public static List<Order> listOrder = new List<Order>();
        SQLiteConnection connection = new SQLiteConnection("Data Source=StoreManagement.db");

        public void getData()
        {
            try
            {
                connection.Open();

                var sql = "SELECT * FROM [Order]";
                var cmd = new SQLiteCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();

                    order.Id = reader.GetInt32(0);
                    order.setCustomer(reader.GetInt32(1));
                    order.setEmployee(reader.GetString(2));
                    order.setPaymentMethod(reader.GetString(3));
                    order.setOrderItem();
                    order.TotalPrice = reader.GetDouble(4);
                    order.setDate(reader.GetString(5));

                    listOrder.Add(order);
                }
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

        public void addOrder(Order order)
        {
            listOrder.Add(order);

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
                    sql = "INSERT INTO OrderProduct (OrderID, ProductName, Quantity, TotalPrice, Category) " +
                        "VALUES (@orderID, @productName, @quantity, @totalPrice, @category)";
                    cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@orderID", orderID);
                    cmd.Parameters.AddWithValue("@productName", orderItem.Name);
                    cmd.Parameters.AddWithValue("@quantity", orderItem.Quantity);
                    cmd.Parameters.AddWithValue("@totalPrice", orderItem.Price);
                    cmd.Parameters.AddWithValue("@category", orderItem.Category);
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

            foreach (var item in listOrder)
            {
                Console.WriteLine($"|{item.Id,-10:d}|" +
                    $"{item.Customer.Id,-12:d}|" +
                    $"{item.Employee.Name,-15:s}|" +
                    $"{item.PaymentMethod.GetType().Name,-30:s}|" +
                    $"{item.TotalPrice,-12:f2}|" +
                    $"{item.Date.ToString("dd/MM/yyyy"),-15:s}|");
            }
            Console.WriteLine();
        }

        public void displayAllOrderByCustomer()
        {
            Console.Write("Enter Customer ID: ");
            int customerID = int.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"___________________________________ORDER CUSTOMER {customerID}____________________________________");
            Console.ResetColor();
            var orderID = "ID";
            var employee = "Employee";
            var paymentMethod = "Payment Method";
            var totalPrice = "Total Price";
            var date = "Date";
            Console.WriteLine($"|{orderID,-10:s}|{employee,-15:s}|{paymentMethod,-30:s}|{totalPrice,-12:s}|{date,-15:s}|");

            foreach (var item in listOrder)
            {
                if (item.Customer.Id == customerID)
                {
                    Console.WriteLine($"|{item.Id,-10:d}|" +
                    $"{item.Employee.Name,-15:s}|" +
                    $"{item.PaymentMethod.GetType().Name,-30:s}|" +
                    $"{item.TotalPrice,-12:f2}|" +
                    $"{item.Date.ToString("dd/MM/yyyy"),-15:s}|");
                }
            }
            Console.WriteLine();
        }

        public void displayOrderDetails()
        {
            Console.Write("Enter Order ID: ");
            int orderID = int.Parse(Console.ReadLine());

            foreach (var item in listOrder)
            {
                if (item.Id == orderID)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"____________________ORDER DETAILS_____________________");
                    Console.ResetColor();
                    Console.WriteLine($"Customer Name: {item.Customer.Name}");
                    Console.WriteLine($"Customer Phone Number: {item.Customer.PhoneNumber}");
                    Console.WriteLine($"Employee Name: {item.Employee.Name}");
                    Console.WriteLine($"Payment Method: {item.PaymentMethod.GetType().Name}");
                    Console.WriteLine($"Total Price: {item.TotalPrice}");
                    Console.WriteLine("Item List: ");
                    foreach (var orderItem in item.OrderItemList)
                    {
                        Console.WriteLine($"{orderItem.Name} - {orderItem.Quantity} - {orderItem.Price}$");
                    }
                    Console.WriteLine();
                    break;
                }
            }
        }
    }
}
