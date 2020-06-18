using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Controllers.User;
using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace BuildWebWithDotNetCore.Controllers.Home
{
    
    public class HomeController : Controller
    {
        [Route("")]
        [Route("user")]
        [Route("user/home")]
        public IActionResult Index()
        {
            //Test data
            var homemodel = new HomeModel();
            ViewBag.headphone = homemodel.getProductbyCategory(1);
            ViewBag.mouse = homemodel.getProductbyCategory(2);
            ViewBag.speaker = homemodel.getProductbyCategory(3);
            ViewBag.keyboard = homemodel.getProductbyCategory(4);
            return View("~/Views/User/Home.cshtml");
        }
    }
}