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

        public override void displayInformation()
        {
            Console.WriteLine(base.Name," ",address);
        }

        
  
        SQLiteConnection conn = new SQLiteConnection("Data Source=StoreManagement.db");
        private void SelectData(SQLiteConnection conn)
        {
            var id = "ID";
            var fullName = "Name";
            var age = "Age";
            var phoneNumber = "PhoneNumber";
            var address = "Address";
            Console.WriteLine($"{id,-20:s}{fullName,-35:s}{age,-20:d}{phoneNumber,10:d}{address,20:d}");
            var sql = "SELECT * FROM Customer";
            var cmd = new SQLiteCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0),-20:d}" +
                    $"{reader.GetString(1),-35:s}" +
                    $"{reader.GetInt32(2),-20:d}" +
                     $"{reader.GetString(3),10:s}" +
                    $"{reader.GetString(4),20:s}");
            }
        }

        public void displayCustomer()
        {
            try
            {
                conn.Open();
                SelectData(conn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
