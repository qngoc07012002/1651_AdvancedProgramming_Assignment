﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.Payment
{
    internal class CashPaymentStrategy : IPaymentStrategy
    {
        private double cashTendered;

        public void CashPayment()
        {

        }

        public void ProcessPayment(double amount)
        {
            throw new NotImplementedException();
        }
    }
}
