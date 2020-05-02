using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
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
            var products = new ProductModel().getProduct();
            var categories = new ProductModel().getCategory();

            var innerJoin = from product in products
                            join category in categories
                            on product.LoaiSP equals category.ID
                            select new ProductModel
                            (
                                product.ID,
                                product.TenSP,
                                category.LoaiSP,
                                product.SoLuong,
                                product.Gia
                            );

            ViewBag.datas = innerJoin;
            return View("~/Views/Admin/ProductPage.cshtml");
        }
    }
}