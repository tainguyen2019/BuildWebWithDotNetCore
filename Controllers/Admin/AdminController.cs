using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Controllers.Admin;
using BuildWebWithDotNetCore.Models;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWebWithDotNetCore.Controllers
{
    [SessionCheck]
    public class AdminController : Controller
    {
        [Route("admin")]
        [Route("admin/index")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/AdminPage.cshtml");
        }
    }
}