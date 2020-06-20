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

namespace BuildWebWithDotNetCore.Controllers.Admin
{
    [SessionCheck]
    public class ProductController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment _environment;

        // Constructor
        [Obsolete]
        public ProductController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }

        [Route("admin/products")]
        [Route("admin/products/index")]
        public IActionResult Index(int page = 1, int category_id=0, string? query="")
        {
            int limit = 6;
            int offset = (page - 1) * limit;

            string myQuery = "";
            if (query != null)
            {
                myQuery = query.Trim();
            }

            ProductModel productModel = new ProductModel();
            List<ProductModel> listProduct = productModel.getProducts(category_id, myQuery);

            int total_product = listProduct.Count();
            ViewBag.total_product = total_product;
            ViewBag.categories = new DatabaseContext().category;
            ViewBag.cate = category_id;
            ViewBag.query = myQuery;
            ViewBag.page = page;
            ViewBag.total_pages = (int)Math.Ceiling((double)total_product / limit);
            ViewBag.datas = listProduct.Skip(offset).Take(limit);
            return View("~/Views/Admin/ProductPage.cshtml");
        }

        [Route("admin/products/detail")]
        public IActionResult Detail(int id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            List<Product> products = databaseContext.product.ToList();
            List<Brand> brands = databaseContext.brand.ToList();

            Product product = products.Where(product => product.product_id == id).First();
            string brand_name = brands.Where(brand => brand.brand_id == product.brand_id).First().brand_name;

            ViewBag.product = product;
            ViewBag.brand_name = brand_name;

            return View("~/Views/Admin/DetailProduct.cshtml");
        }

        [Route("admin/products/edit")]
        public IActionResult ShowFormEditProduct(int id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            List<Product> products = databaseContext.product.ToList();
            Product product = products.Where(product => product.product_id == id).First();
            var categories = databaseContext.category;
            
            ViewBag.categories = categories;
            ViewBag.product = product;

            return View("~/Views/Admin/EditProduct.cshtml");
        }

        [HttpPost]
        [Route("admin/products/edit_product")]
        [Obsolete]
        public IActionResult EditProduct(int product_id, string product_name, int category, int brand, int quantity
                                       , double cost, string description)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var product = databaseContext.product.Where(product => product.product_id == product_id).FirstOrDefault();
            if (product != null)
            {
                product.product_name = product_name;
                product.category_id = category;
                product.brand_id = brand;
                product.quantity = quantity;
                product.price = cost;
                product.description = description;
                databaseContext.SaveChanges();
            }
            return Redirect("~/admin/products");

        }

        [Route("admin/products/delete")]
        [Obsolete]
        public IActionResult DeleteProduct(int id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var product = databaseContext.product.Where(product => product.product_id == id).FirstOrDefault();
            if (product != null)
            {
                string webRootPath = _environment.WebRootPath;
                string fullImagePath = Path.Combine(webRootPath + "\\images", product.image);

                if (System.IO.File.Exists(fullImagePath))
                {
                    try
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        System.IO.File.Delete(fullImagePath);
                    }
                    catch (Exception e) { }
                }
                databaseContext.Remove(product);
                databaseContext.SaveChanges();
            }
            return Redirect("~/admin/products");

        }

        [Route("admin/products/new")]
        public IActionResult ShowFormAddProduct()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var categories = databaseContext.category;
            ViewBag.categories = categories;
            return View("~/Views/Admin/CreateProduct.cshtml");

        }

        [HttpPost]
        [Route("admin/products/insert_product")]
        [Obsolete]
        public IActionResult AddProduct(string product_name, int category, int brand, int quantity
                                        , double cost, string description, IFormFile image)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            List<Product> productDB = databaseContext.product.ToList();
            Product lastProduct = productDB.Last();
            int ProductID = lastProduct.product_id + 1;
            string file = image.FileName;
            string extension = file.Split(".").Last();
            string fileName = "";
            switch (category) {
                case 1: { fileName = "tainghe"; break; }
                case 2: { fileName = "chuot"; break; }
                case 3: { fileName = "loa"; break; }
                case 4: { fileName = "banphim"; break; }
            }
            string newFile = fileName + "-" + ProductID + "." + extension;
            string path = Path.Combine(_environment.WebRootPath, "images") + $@"\{newFile}";
            using (FileStream fs = System.IO.File.Create(path))
            {
                image.CopyTo(fs);
                fs.Flush();
            }

            Product product = new Product(ProductID, product_name, category, brand, quantity, cost, newFile, 1, description);
            databaseContext.product.Add(product);
            databaseContext.SaveChanges();
            return Redirect("~/admin/products");

        }

        [HttpPost]
        [Route("admin/products/get_brands_form")]
        public IActionResult GetBrandsForm(int category)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var brands = databaseContext.brand;
            var category_brand = databaseContext.category_brand;
            List<Brand> result = (from brand in brands
                                  join categorybrand in category_brand
                                  on brand.brand_id equals categorybrand.brand_id
                                  where categorybrand.category_id == category
                                  select new Brand(brand.brand_id, brand.brand_name)).ToList();
            ViewBag.brands = result;

            return View("~/Views/Admin/BrandForm.cshtml");
        }
    }
}