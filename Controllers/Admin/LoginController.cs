using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWebWithDotNetCore.Controllers.Admin
{
    public class LoginController : Controller
    {
        [Route("admin/login")]
        [Route("admin/login/index")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/LoginPage.cshtml");
        }


        [Route("admin/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("isLogin");
            return Redirect("login");
        }


        [HttpPost]
        [Route("admin/login")]
        public IActionResult checkLogin(string email, string password)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var data = databaseContext.account;
            var result = data.Where(account => account.email == email 
                                                && account.password == password 
                                                && account.role == 1)
                        .ToArray();

            if (result.Length > 0)
            {
                HttpContext.Session.SetString("isLogin", "ok");
                return Redirect("/admin");

            }
            else
            {
                return Redirect("/admin/login");
            }
        }
    }
}