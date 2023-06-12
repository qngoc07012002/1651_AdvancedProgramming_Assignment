using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.PersonModel
{
    internal class Customer : Person
    {
        private string address;

        public string Address { get { return address; } set { address = value; } }

        public override void enterInformation()
        {
            Console.Write("Enter Name: ");
            base.Name = Console.ReadLine();
            Console.Write("Enter Age: ");
            base.Age = int.Parse(Console.ReadLine());
            Console.Write("Enter Phone Number: ");
            base.PhoneNumber = Console.ReadLine();
            Console.Write("Enter Address: ");
            Address = Console.ReadLine();
        }

        public override void displayInformation()
        {
            Console.WriteLine(base.Name," ",address);
        }

        
  

    }
}
