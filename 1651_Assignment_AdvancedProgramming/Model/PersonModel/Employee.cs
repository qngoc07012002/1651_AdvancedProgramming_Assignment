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

        public override void  enterInformation()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_Enter Employee Information_");
            Console.ResetColor();

            Console.Write("Enter Name: ");
            base.Name = Console.ReadLine();
            Console.Write("Enter Position: ");
            Position = Console.ReadLine();
        }

        public override void displayInformation()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Welcome, {Position} {base.Name}!");
            Console.ResetColor();
        }
    }
}
