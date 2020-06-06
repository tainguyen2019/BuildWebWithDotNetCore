using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using BuildWebWithDotNetCore.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Controllers.User
{
    public class UserLoginControler : Controller
    {
        [Route("/login")]
        [Route("/login/index")]
        public IActionResult Index()
        {
            return View("~/Views/User/Login.cshtml");
        }

        [Route("/logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Remove("isUserLogin");
            return Redirect("login");
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult checkLogin(string email, string password)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var data = databaseContext.account;
            var result = data.Where(account => account.email == email
                                                && account.password == password
                                                && account.role == 2)
                        .ToArray();

            if (result.Length > 0)
            {
                HttpContext.Session.SetString("isUserLogin", "ok");
                return Redirect("~/");
            }
            else
            {
                return Redirect("/login");
            }
        }

        [Route("/register")]
        public IActionResult register()
        {
            return View("~/Views/User/Register.cshtml");
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult createUser(string email, string password, string username, string phone, string address)
        {

            DatabaseContext databaseContext = new DatabaseContext();
            var UserLogin = new UserLoginModel();
            
            // add new user account
            Account user = new Account(UserLogin.getLatestAccountId(), email, password,2);
            // add new customer info
            Customer customer = new Customer(username, phone, address, UserLogin.getLatestAccountId());
            databaseContext.account.Add(user);
            databaseContext.customer.Add(customer);
            databaseContext.SaveChanges();
            return Redirect("/login");
        }
    }
}
