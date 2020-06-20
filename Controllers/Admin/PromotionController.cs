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
    public class PromotionController : Controller
    {

        [Route("admin/promotions")]
        [Route("admin/promotions/index")]
        public IActionResult Index(int page = 1)
        {
            int limit = 6;
            int offset = (page - 1) * limit;
            PromotionModel promotionModel = new PromotionModel();
            List<PromotionModel> list = promotionModel.getPromotions();

            int total_promotion = list.Count();
            ViewBag.total_promotion = total_promotion;
            ViewBag.page = page;
            ViewBag.total_pages = (int)Math.Ceiling((double)total_promotion / limit);
            ViewBag.datas = list.Skip(offset).Take(limit);

            return View("~/Views/Admin/PromotionPage.cshtml");
        }

        [Route("admin/promotions/edit")]
        public IActionResult ShowFormEditPromotion(string promotion_code)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var products = databaseContext.product;
            var promotions = databaseContext.promotion;

            Promotion promotion = promotions.Where(promotion => promotion.promotion_code == promotion_code).First();
            
            ViewBag.products = products;
            ViewBag.promotion = promotion;

            return View("~/Views/Admin/EditPromotion.cshtml");
        }

        [HttpPost]
        [Route("admin/promotions/edit_promotion")]
        public IActionResult EditPromotion(string promotion_code, int product, DateTime valid_date
                                            , DateTime expiry_date, float discount)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var promotion = databaseContext.promotion.Where(p => p.promotion_code == promotion_code).FirstOrDefault();
            if (promotion != null)
            {
                promotion.product_id = product;
                promotion.discount = (float)Convert.ToDouble(discount / 100.0);
                promotion.valid_date = valid_date;
                promotion.expiry_date = expiry_date;
                databaseContext.SaveChanges();
            }
            return Redirect("~/admin/promotions");

        }

        [Route("admin/promotions/delete")]
        public IActionResult DeleteProduct(string promotion_code)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var promotion = databaseContext.promotion.Where(p => p.promotion_code == promotion_code).FirstOrDefault();
            if (promotion != null)
            {
                
                databaseContext.Remove(promotion);
                databaseContext.SaveChanges();
            }
            return Redirect("~/admin/promotions");

        }

        [Route("admin/promotions/new")]
        public IActionResult ShowFormAddPromotion()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var products = databaseContext.product;

            PromotionModel promotionModel = new PromotionModel();
            string promotion_code = promotionModel.generatePromotionCode();

            ViewBag.products = products;
            ViewBag.promotion_code = promotion_code;
            
            return View("~/Views/Admin/CreatePromotion.cshtml");

        }

        [HttpPost]
        [Route("admin/promotions/insert_promotion")]
        public IActionResult AddPromotion(string promotion_code, int discount, int product, DateTime valid_date
                                        , DateTime expiry_date)
        {
            DatabaseContext databaseContext = new DatabaseContext();

            Promotion promotion = new Promotion(promotion_code, product, (float)Convert.ToDouble(discount/100.0)
                                                , valid_date, expiry_date);
            databaseContext.promotion.Add(promotion);
            databaseContext.SaveChanges();
            return Redirect("~/admin/promotions");

        }
    }
}