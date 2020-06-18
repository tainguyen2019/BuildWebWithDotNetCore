using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.User
{
    public class UserLoginModel
    {
        public UserLoginModel() { }

        public int getLatestAccountId()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var account = databaseContext.account;
            Account latestAccount = account.OrderByDescending(account => account.account_id).First();
            int newAccountId = latestAccount.account_id + 1;
            return newAccountId;
        }
    }
}
