using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Account
    {
        private string email { get; set; }
        private string password { get; set; }

        public Account() { }

        public Account(string Email, string Password)
        {
            email = Email;
            password = Password;
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
    }
}
