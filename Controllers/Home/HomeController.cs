using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View("~/Views/Home/Index.cshtml");
        }
    }
}