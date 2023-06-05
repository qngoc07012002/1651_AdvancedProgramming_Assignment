using _1651_Assignment_AdvancedProgramming.Model.Payment;
using _1651_Assignment_AdvancedProgramming.Model.PersonModel;
using _1651_Assignment_AdvancedProgramming.Model.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.Order
{
    internal class Order
    {
        private int id;
        private DateTime date;
        private Customer customer;
        private Employee employee;
        private IPaymentStrategy paymentMethod;
        private List<Product> orderItemList;

        public int Id { get { return id; } set { id = value; } }

        public void addEmployeeInformation(Employee employee)
        {

        }

        public void addCustomerInformation(Customer customer)
        {

        }

        public void addProductToOrder()
        {

        }

        public void addPaymentMethod()
        {

        }

        public void processCheckout() 
        {

        }
    }
}
