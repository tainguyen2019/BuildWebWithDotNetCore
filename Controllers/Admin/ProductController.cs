using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWebWithDotNetCore.Controllers.Admin
{
    [SessionCheck]
    public class ProductController : Controller
    {

        [Route("admin/products")]
        [Route("admin/products/index")]
        public IActionResult Index()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var products = databaseContext.product;
            var categories = databaseContext.category;

            var innerJoin = from product in products
                            join category in categories
                            on product.category_id equals category.category_id
                            select new ProductModel
                            (
                                product.product_id,
                                product.product_name,
                                category.category_name,
                                product.quantity,
                                product.price
                            );

            ViewBag.datas = innerJoin;
            return View("~/Views/Admin/ProductPage.cshtml");
        }
    }
}