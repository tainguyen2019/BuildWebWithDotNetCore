using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BuildWebWithDotNetCore.Models.Admin;
using BuildWebWithDotNetCore.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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