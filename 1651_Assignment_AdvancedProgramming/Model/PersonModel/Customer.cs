using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.PersonModel
{
    internal class Customer : Person
    {
        private string address;

        public string Address { get { return address; } set { address = value; } }

        public override void displayInformation()
        {
            Console.WriteLine(base.Name," ",address);
        }
    }
}
