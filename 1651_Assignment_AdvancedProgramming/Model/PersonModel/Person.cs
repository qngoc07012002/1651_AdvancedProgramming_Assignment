using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1651_Assignment_AdvancedProgramming.Model.PersonModel
{
    internal abstract class Person
    {
        private int id;
        private string name;
        private string age;
        private string phoneNumber;
        
        public int Id { get { return id; }  }
        public string Name { get { return name; } set { name = value; } }
        public string Age { get { return age; } set { age = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }

        public abstract void displayInformation();
    }
}
