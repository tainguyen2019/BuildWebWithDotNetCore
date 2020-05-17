using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Account
    {
        public int account_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }

        public Account() { }

        public Account(int AccountID, string Email, string Password, int Role)
        {
            this.account_id = AccountID;
            this.email = Email;
            this.password = Password;
            this.role = Role;
        }

    }
}
