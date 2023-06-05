using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.PersonModel
{
    internal class Employee : Person
    {
        private string position;

        public string Position { get { return position; } set { position = value; } }

        public override void displayInformation()
        {
            Console.WriteLine(base.Name, " ", position);
        }
    }
}
