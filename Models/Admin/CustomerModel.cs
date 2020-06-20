using BuildWebWithDotNetCore.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Admin
{
    public class CustomerModel
    {
        private string phone;
        private string name;
        private string address;
        private string email;
        private string password;

        public CustomerModel() { }
        public CustomerModel(string Phone, string Name, string Address, string Email, string Password)
        {
            this.address = Address;
            this.email = Email;
            this.phone = Phone;
            this.password = Password;
            this.name = Name;
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public List<CustomerModel> getCustomers(string query)
        {
            query = query.ToLower();
            DatabaseContext databaseContext = new DatabaseContext();
            var customer = databaseContext.customer;
            var account = databaseContext.account;

            List<CustomerModel> result = (from c in customer
                                          join a in account
                                          on c.account_id equals a.account_id
                                          where c.customer_name.Contains(query)
                                                            || c.address.Contains(query)
                                                            || c.phone.Contains(query)
                                                            || a.email.Contains(query)
                                                            || a.password.Contains(query)
                                          select new CustomerModel
                                          (
                                            c.phone,
                                            c.customer_name,
                                            c.address,
                                            a.email,
                                            a.password
                                           )).ToList();


            return result;
        }
    }
}
