using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using BuildWebWithDotNetCore.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Helpers;

namespace BuildWebWithDotNetCore.Controllers.User
{
    public class UserLoginControler : Controller
    {
        [Route("/login")]
        [Route("/login/index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                return View("~/Views/User/Order_Login.cshtml");
            }
            return View("~/Views/User/Login.cshtml");
        }

        [Route("/logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Remove("isUserLogin");
            SessionHelper.SetObjectAsJson(HttpContext.Session, "customer", null);
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
                var customerData = databaseContext.customer;

                var cus = customerData.Where(cus => cus.account_id == result[0].account_id
                                                )
                        .ToArray();
                if(cus.Length > 0)
                {
                    HttpContext.Session.SetString("isUserLogin", "ok");

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "customer", new CustomerSession {
                        account_id = result[0].account_id,
                        email = result[0].email,
                        name = cus[0].customer_name,
                        phone = cus[0].phone,
                        address = cus[0].address
                    });

                }
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                if (cart != null)
                {
                    return Redirect("~/cart");
                }
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
