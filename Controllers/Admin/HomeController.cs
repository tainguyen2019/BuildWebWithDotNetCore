using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace BuildWebWithDotNetCore.Controllers
{
    public class HomeController : Controller
    {

        [Route("admin")]
        [Route("admin/index")]

        public IActionResult Index()
        {
            //var data = new HomeModel().Test();
            //var result = data.Where(account => account.Email == "admin@gmail.com");
            //ViewBag.data = result;

            return View("~/Views/Admin/AdminPage.cshtml");
        }

        [Route("admin/home/welcome")]
        [Route("admin/home/welcome/{message?}")]
        public IActionResult Welcome(string message = "Hello Tai Nguyen")
        {
            ViewData["message"] = message;
            return View("~/Views/Admin/AdminPage.cshtml");
        }

        [Route("admin/home.test")]
        [Route("admin/home/test/{id?}")]
        public IActionResult TestAdmin(int? id)
        {
            ViewBag.id = id;

            return View("~/Views/Admin/AdminPage.cshtml");
        }
    }
}