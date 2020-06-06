using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Customer
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int account_id { get; set; }

        public Customer() { }

        public Customer( string CustomerName, string Phone, string Address, int AccountID)
        {
            this.customer_name = CustomerName;
            this.phone = Phone;
            this.address = Address;
            this.account_id = AccountID;
        }

    }
}
