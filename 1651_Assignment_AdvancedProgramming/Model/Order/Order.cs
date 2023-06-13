using _1651_Assignment_AdvancedProgramming.Controller;
using _1651_Assignment_AdvancedProgramming.Model.Payment;
using _1651_Assignment_AdvancedProgramming.Model.PersonModel;
using _1651_Assignment_AdvancedProgramming.Model.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.Order
{
    internal class Order
    {
        private int id;
        private Customer customer;
        private Employee employee;
        private IPaymentStrategy paymentMethod;
        private List<Product> orderItemList = new List<Product>();
        private double totalPrice;
        private DateTime date;

        public int Id { get { return id; }  }
        public Customer Customer { get { return customer; }  }
        public Employee Employee { get { return employee; }  }
        public IPaymentStrategy PaymentMethod { get { return paymentMethod; } }
        public IReadOnlyCollection<Product> OrderItemList { get { return orderItemList.AsReadOnly(); } }
        public double TotalPrice { get { return totalPrice; }  }
        public DateTime Date { get { return date; } }

        public void createOrder()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Add Product_");
            Console.ResetColor();
            date = DateTime.Now;
            addCustomerInformation();
            addProductToOrder();
            addPaymentMethod();
            processCheckout();
            foreach (var item in orderItemList)
            {
                Console.WriteLine($"{item.Name} {item.Quantity} {item.Price}");
            }
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

                do
                {
                    Console.Write("Enter Quantity: ");
                    product.Quantity = int.Parse(Console.ReadLine());

                    if (stock - product.Quantity >= 0)
                    {
                       productController.updateQuantityById(id, stock - product.Quantity);
                       product.Price = product.Quantity*product.Price;
                        
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
