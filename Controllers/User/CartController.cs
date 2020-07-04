using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildWebWithDotNetCore.Helpers;
using BuildWebWithDotNetCore.Models.Home;
using BuildWebWithDotNetCore.Models.Database;


namespace BuildWebWithDotNetCore.Controllers.Home
{
    [Route("cart")]
    public class CartController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart != null ? cart : null;
            if (cart != null)
            {
                ViewBag.totalPrice = Math.Round(cart.Sum(item => item.Quantity * item.Product.price - item.Product.price*item.discount));
            }
            else
            {
                ViewBag.totalPrice = 0;
            }
            return View("~/Views/User/Cart.cshtml");
        }

        [Route("buy/{id}")]
        public IActionResult buy(string id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var products = databaseContext.product;
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();

                Product p = products.First(i => i.product_id == Int16.Parse(id));
                cart.Add(new Item { Product = p, Quantity = 1, discount=0 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    Product p = products.First(i => i.product_id == Int16.Parse(id));

                    cart.Add(new Item { Product = p, Quantity = 1, discount=0 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
          
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                return RedirectToAction("Index");
            }

            int index = isExist(id);
            if (index != -1)
            {
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");

        }

        [Route("sub/{id}")]
        public IActionResult Sub(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");


            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Quantity == 1)
                {
                    return RedirectToAction("Remove", new { id = id });
                }
                cart[index].Quantity--;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");

        }

        [Route("add/{id}")]
        public IActionResult Add(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");


            int index = isExist(id);
            if (index != -1)
            {

                cart[index].Quantity++;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");

        }

        [Route("discount/{code}")]
        public IActionResult Discount(string code)
        {
            try
            {
                DatabaseContext databaseContext = new DatabaseContext();
                var promotions = databaseContext.promotion;
                Promotion pr = promotions.Where(i => i.promotion_code.Equals(code)).FirstOrDefault();

                if (pr == null)
                {
                    return new JsonResult(new Item { Product=null, Quantity=0,discount=0});

                }

                if(pr.expiry_date < DateTime.Now || pr.valid_date >DateTime.Now)
                {
                    return new JsonResult(new Item { Product = null, Quantity = 0, discount = 0 });

                }

                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");


                int index = isExist(pr.product_id.ToString());
                if (index != -1 && cart[index].discount ==0)
                {
                    
                    cart[index].discount = pr.discount;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    var totalPrice = Math.Round(cart.Sum(item => item.Quantity * item.Product.price - item.Product.price * item.discount));
                    return new JsonResult(new { item= cart[index], totalPrice });
                }

                return new JsonResult(new Item { Product = null, Quantity = 0, discount = 0 });

            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;

        }

        private int isExist(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.product_id.Equals(Int16.Parse(id)))
                {
                    return i;
                }
            }
            return -1;
        }



    }
}
