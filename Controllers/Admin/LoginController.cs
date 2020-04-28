using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        [Route("admin/login")]
        public IActionResult checkLogin(string email,string password)
        {
            var data = new HomeModel().Test();
            var result = data.Where(account => account.Email == email && account.Password == password).ToArray();
            if(result.Length > 0)
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