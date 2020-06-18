using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Controllers.Admin;
using BuildWebWithDotNetCore.Models;
using BuildWebWithDotNetCore.Models.Admin;
using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BuildWebWithDotNetCore.Controllers
{
    [SessionCheck]
    public class AdminController : Controller
    {
        [Route("admin")]
        [Route("admin/index")]
        public IActionResult Index()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var sale_order = databaseContext.sale_order;
            var sale_order_line = databaseContext.sale_order_line;
            int product_card = (from so in sale_order
                          join sol in sale_order_line
                          on so.order_id equals sol.order_id
                          where so.status == 2 && DateTime.Now.Month == so.create_date.Month
                          && DateTime.Now.Year == so.create_date.Year
                          select sol.quantity).Sum();

            double revenue_card = (from so in sale_order
                                   where so.status == 2 && DateTime.Now.Month == so.create_date.Month
                                   && DateTime.Now.Year == so.create_date.Year
                                   select so.total).Sum();

            int order_card = (from so in sale_order
                              where so.status == 2 && DateTime.Now.Month == so.create_date.Month
                              && DateTime.Now.Year == so.create_date.Year
                              select so.order_id).Count();

            int customer_card = sale_order.Select(so => so.customer_id).Distinct().Count();


            ViewBag.product_card = product_card;
            ViewBag.order_card = order_card;
            ViewBag.revenue_card = revenue_card;
            ViewBag.customer_card = customer_card;
            return View("~/Views/Admin/AdminPage.cshtml");
        }

        [Route("get-data-chart")]
        public ContentResult GetData()
        {
            List<Point> result = new AdminModel().getDataChart();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

    }
}