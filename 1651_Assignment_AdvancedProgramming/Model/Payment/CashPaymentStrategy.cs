using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.Payment
{
    internal class CashPaymentStrategy : IPaymentStrategy
    {
        private double cashTendered;

        public void ProcessPayment(double amount)
        {
            do
            {
                Console.Write("Enter Tendered: ");
                cashTendered = double.Parse(Console.ReadLine());

                if (cashTendered < amount)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong, Please try again!");
                    Console.ResetColor();
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Excess money returned: {cashTendered - amount}$");           
                    Console.ResetColor();
                }
            } while (cashTendered < amount);
        }
    }
}
