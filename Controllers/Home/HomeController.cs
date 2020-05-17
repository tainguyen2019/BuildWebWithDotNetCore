using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace BuildWebWithDotNetCore.Controllers.Home
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        [Route("home/index")]
        public IActionResult Index()
        {
            //Test data
            DatabaseContext databaseContext = new DatabaseContext();
            ViewBag.data = databaseContext.product;
            return View("~/Views/Home/Index.cshtml");
        }
    }
}