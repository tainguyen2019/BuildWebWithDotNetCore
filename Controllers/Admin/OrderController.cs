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
    public class OrderController : Controller
    {
        [Route("admin/orders")]
        [Route("admin/orders/index")]
        public IActionResult Index(int page = 1)
        {
            int limit = 6;
            int offset = (page - 1) * limit;

            DatabaseContext databaseContext = new DatabaseContext();
            List<SaleOrder> orders = databaseContext.sale_order.ToList();
            
            int total_order = orders.Count();
            ViewBag.total_order = total_order;
            ViewBag.page = page;
            ViewBag.total_pages = (int)Math.Ceiling((double)total_order / limit);
            ViewBag.datas = orders.Skip(offset).Take(limit);
            return View("~/Views/Admin/OrderPage.cshtml");
        }

        [Route("admin/orders/detail")]
        public IActionResult Detail(int order_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            List<SaleOrder> saleOrders = databaseContext.sale_order.ToList();
            List<Product> products = databaseContext.product.ToList();
            List<SaleOrderLine> saleOrderLines = databaseContext.sale_order_line.ToList();

            SaleOrder sale_order = saleOrders.Where(order => order.order_id == order_id).First();
            List<SaleOrderLine> sale_order_line = saleOrderLines.Where(sale_order_line => sale_order_line.order_id == order_id).ToList();
            List<OrderModel> sale_order_lines = (from product in products
                                                 join line in sale_order_line
                                                 on product.product_id equals line.product_id
                                                 orderby product.product_id descending
                                                 select new OrderModel
                                                 (
                                                    product.product_name,
                                                    line.quantity,
                                                    line.price,
                                                    line.discount,
                                                    line.amount
                                                    )).ToList();

            ViewBag.sale_order = sale_order;
            ViewBag.sale_order_lines = sale_order_lines;

            return View("~/Views/Admin/DetailOrder.cshtml");
        }


        [Route("admin/orders/confirm")]
        public IActionResult ConfirmOrder(int order_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var order = databaseContext.sale_order.Where(order => order.order_id == order_id).FirstOrDefault();
            if (order != null)
            {
                order.status = 2;
                databaseContext.SaveChanges();
            }
            return Redirect("~/admin/orders");

        }

        [Route("admin/orders/cancel")]
        public IActionResult CancelOrder(int order_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var order = databaseContext.sale_order.Where(order => order.order_id == order_id).FirstOrDefault();
            if (order != null)
            {
                order.status = 3;
                databaseContext.SaveChanges();
            }
            return Redirect("~/admin/orders");
        }

    }
}