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
            productController.getData();
            CustomerController customerController = new CustomerController();
            customerController.getData();
            OrderController orderController = new OrderController();
            orderController.getData();
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
                        orderController.addOrder(order);
                        break;
                    case 10:
                        orderController.displayAllOrder();
                        break;
                    case 11:
                        orderController.displayAllOrderByCustomer();
                        break;
                    case 12:
                        orderController.displayOrderDetails();
                        break;
                    case 13:
                        Console.Clear();
                        employee.displayInformation();
                        displayMenu();
                        break;
                    case 14:
                        Environment.Exit(0);
                        break;
                }
            } while (choice != 13);

        }

        public static void loadingBar()
        {
            Console.ForegroundColor = ConsoleColor.Green;
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
            Console.ResetColor();
        }

        public static void displayMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_____Customer_____");
            Console.ResetColor();
            Console.WriteLine("1.Display All Customer");
            Console.WriteLine("2.Add Customer");
            Console.WriteLine("3.Edit Customer");
            Console.WriteLine("4.Remove Customer");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_____Product_____");
            Console.ResetColor();
            Console.WriteLine("5.Display All Product");
            Console.WriteLine("6.Add Product");
            Console.WriteLine("7.Edit Product");
            Console.WriteLine("8.Remove Product");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_____Order_____");
            Console.ResetColor();
            Console.WriteLine("9.Create Order");
            Console.WriteLine("10.Display All Order");
            Console.WriteLine("11.Display All Order By Customer");
            Console.WriteLine("12.Display Order Details");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_____Setting_____");
            Console.ResetColor();
            Console.WriteLine("13.Clear Screen");
            Console.WriteLine("14.Exit");
            Console.WriteLine();
        }
    }
}
