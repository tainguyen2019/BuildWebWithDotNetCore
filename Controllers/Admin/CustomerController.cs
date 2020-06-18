using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using BuildWebWithDotNetCore.Models.Admin;

namespace BuildWebWithDotNetCore.Controllers.Admin
{
    [SessionCheck]
    public class CustomerController : Controller
    {
        [Route("admin/customers")]
        [Route("admin/customers/index")]
        public IActionResult Index(int page = 1, string? query="")
        {
            int limit = 6;
            int offset = (page - 1) * limit;

            string myQuery = "";
            if (query != null)
            {
                myQuery = query.Trim();
            }

            CustomerModel customerModel = new CustomerModel();
            List<CustomerModel> listCustomer = customerModel.getCustomers(myQuery);

            int total_customer = listCustomer.Count();
            ViewBag.total_customer = total_customer;
            ViewBag.query = myQuery;
            ViewBag.page = page;
            ViewBag.total_pages = (int)Math.Ceiling((double)total_customer / limit);
            ViewBag.datas = listCustomer.Skip(offset).Take(limit);
            return View("~/Views/Admin/CustomerPage.cshtml");
        }

    }
}