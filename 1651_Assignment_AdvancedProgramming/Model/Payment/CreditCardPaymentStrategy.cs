using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.Payment
{
    internal class CreditCardPaymentStrategy : IPaymentStrategy
    {
        private string number;
        private string ccv;
        private string expDate;

        private bool Authorized()
        {
            string regexDate = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/((19|20)\d{2})$";
            if (number.Length == 16)
            {
                if (ccv.Length == 3 || ccv.Length == 4)
                {
                    if (Regex.IsMatch(expDate, regexDate))
                    {
                        return true;
                    }
                }

            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Something went wrong, Please try again!");
            Console.ResetColor();
            return false;
        }

        public void ProcessPayment(double amount)
        {
            do
            {
                Console.Write("Number: ");
                number = MaskInput();
                Console.Write("CCV: ");
                ccv = MaskInput();
                Console.Write("expDate(dd/mm/yyyy): ");
                expDate = Console.ReadLine();
                Console.WriteLine(number);
            } while (Authorized() == false);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Payment Processing Successful!");
            Console.ResetColor();
            Console.WriteLine();
        }

        private string MaskInput()
        {
            StringBuilder ccvBuilder = new StringBuilder();
            int count = 0; 
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Enter)
                {
                    if (keyInfo.KeyChar != '\0')
                    {
                        if (count > 0 && count % 4 == 0)
                        {
                            Console.Write(" "); 
                        }

                        Console.Write("*");
                        ccvBuilder.Append(keyInfo.KeyChar);
                        count++;
                    }
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return ccvBuilder.ToString();
        }
    }
}
