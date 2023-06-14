using _1651_Assignment_AdvancedProgramming.Controller;
using _1651_Assignment_AdvancedProgramming.Model.Payment;
using _1651_Assignment_AdvancedProgramming.Model.PersonModel;
using _1651_Assignment_AdvancedProgramming.Model.ProductModel;
using _1651_Assignment_AdvancedProgramming.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.Order
{
    internal class Order
    {
        private int id;
        private Customer customer = new Customer();
        private Employee employee = new Employee();
        private IPaymentStrategy paymentMethod;
        private List<Product> orderItemList = new List<Product>();
        private double totalPrice;
        private DateTime date;

        public int Id { get { return id; } set { id = value; } }

        public Customer Customer { get { return customer; } }

        public void setCustomer(int id)
        {
            CustomerController customerController = new CustomerController();

            customer = customerController.getCustomerByID(id);
        }

        public Employee Employee { get { return employee; }  }

        public void setEmployee(string name)
        {
            employee.Name = name;
        }

        public IPaymentStrategy PaymentMethod { get { return paymentMethod; } }

        public void setPaymentMethod(string method)
        {
            switch (method)
            {
                case "CreditCardPaymentStrategy":
                    paymentMethod = new CreditCardPaymentStrategy();
                    break;
                case "CashPaymentStrategy":
                    paymentMethod = new CashPaymentStrategy();
                    break;
            }
        }

        public List<Product> OrderItemList { get { return orderItemList; } }
        
        public void setOrderItem()
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=StoreManagement.db");
            ProductController productController = new ProductController();

            try
            {
                connection.Open();

                var sql = "SELECT * FROM OrderProduct WHERE OrderID = @orderID";
                var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@orderID", id);
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

                    product.Name = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    product.Price = reader.GetDouble(3);
                    product.Category = category;

                    orderItemList.Add(product);
                   
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred while getting order items: " + ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                connection.Close();
            }


        }

        public double TotalPrice { get { return totalPrice; } set { totalPrice = value; } }

        public DateTime Date { get { return date; } }

        public void setDate(string dateString)
        {
            date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public void createOrder()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Create Order_");
            Console.ResetColor();
            date = DateTime.Now;
            addCustomerInformation();
            addProductToOrder();
            addPaymentMethod();
            processCheckout();
        }

        public void addEmployeeInformation(Employee employee)
        {
            this.employee = employee;
        }

        public void addCustomerInformation()
        {
            do
            {
                Console.Write("Enter Customer ID: ");
                int customerID = int.Parse(Console.ReadLine());
                CustomerController customerController = new CustomerController();
                customer = customerController.getCustomerByID(customerID);
            } while (customer == null);
            Console.WriteLine();
        }

        public void addProductToOrder()
        {
            ProductController productController = new ProductController();

            Console.Write("Enter Number of Product: ");
            int numberofProduct = int.Parse(Console.ReadLine());
            Console.WriteLine();

            for (int i = 0; i < numberofProduct; i++)
            {
                Console.Write($"Product ID : ");
                int id = int.Parse(Console.ReadLine());
                
                Product product = productController.getProductByID(id);
                int stock = product.Quantity;
                int quantityProduct = 0;
                do
                {
                    Console.Write("Enter Quantity: ");
                    quantityProduct = int.Parse(Console.ReadLine());

                    if (stock - quantityProduct >= 0)
                    {
                       productController.updateQuantityById(id, stock - quantityProduct);
                       product.Price = quantityProduct * product.Price;
                       product.Quantity = quantityProduct;
                       totalPrice += product.Price;
                       orderItemList.Add(product);
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Out of stock, Please try again!");
                        Console.ResetColor();
                    }
                } while (stock - product.Quantity < 0);
            }

            Console.WriteLine();
        }

        public void addPaymentMethod()
        {
            Console.WriteLine("Payment Method");
            Console.WriteLine("1. Cash Payment");
            Console.WriteLine("2. Credit Card Payment");
            Console.WriteLine();
            int choice = 0;
            do
            {
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());
                if (choice != 1 && choice != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong, Please try again!");
                    Console.ResetColor();
                }
            } while (choice != 1 && choice != 2);

            switch (choice)
            {
                case 1:
                    paymentMethod = new CashPaymentStrategy();
                    break;
                case 2:
                    paymentMethod = new CreditCardPaymentStrategy();
                    break;
            }

            paymentMethod.ProcessPayment(totalPrice);
        }

        public void processCheckout() 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Processing ");
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < 2)
            {
                Console.Write(".");
                Thread.Sleep(200);

            }
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine("Create Completed Order!");
            Console.ResetColor();
        }
    }
}
