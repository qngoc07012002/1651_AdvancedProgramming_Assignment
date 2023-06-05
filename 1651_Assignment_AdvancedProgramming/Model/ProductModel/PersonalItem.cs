using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.ProductModel
{
    internal class PersonalItem : Product
    {
        public override string getCategory()
        {
            return this.GetType().Name;
        }
    }
}
