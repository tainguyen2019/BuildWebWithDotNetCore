using BuildWebWithDotNetCore.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Controllers.User
{
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("product/showproduct/{category_id?}/{brand_id?}")]
        public IActionResult showproduct(int category_id, int page = 1, int brand_id = 0)
        {
            int limit = 6;
            int offset = (page - 1) * limit;
            var products = new ProductsModel();
            var productList = products.showProducts(category_id, brand_id);
            int total_product = productList.Count();
            ViewBag.products = productList;
            ViewBag.category = category_id;
            ViewBag.brands = products.showBrands(category_id); 
            ViewBag.total_product = total_product;
            ViewBag.page = page;
            ViewBag.total_pages = (int)Math.Ceiling((double)total_product / limit);
            ViewBag.datas = productList.Skip(offset).Take(limit);
            //Test data
            return View("~/Views/User/Products.cshtml");
        }
        
        [Route("product/showproductDetail/{product_id?}")]
        public IActionResult showProductDetail(int product_id)
        {
            var product = new ProductsModel();
            ViewBag.product = product.showProductDetail(product_id);
            return View("~/Views/User/DetailProduct.cshtml");
        }

        [HttpGet]
        [Route("product/searchProduct/{product_name?}")]
        public IActionResult searchProduct(string product_name)
        {
            var product = new ProductsModel();
            ViewBag.product = product.searchProduct(product_name);
            return View("~/Views/User/DetailProduct.cshtml");
        }
    }
}
