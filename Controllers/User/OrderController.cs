using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildWebWithDotNetCore.Helpers;
using BuildWebWithDotNetCore.Models.Home;
using BuildWebWithDotNetCore.Models.Database;
using MailKit.Net.Smtp;
using MimeKit;
using BuildWebWithDotNetCore.Models.User;
using System.IO;
using BuildWebWithDotNetCore.Models.Admin;


namespace BuildWebWithDotNetCore.Controllers.User
{
    [Route("order")]
    public class OrderController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            var customer = SessionHelper.GetObjectFromJson<CustomerSession>(HttpContext.Session, "customer");

            if (customer == null)
            {
                return Redirect("~/login");
            }

            ViewBag.cart = cart != null ? cart : null;
            ViewBag.customer = customer != null ? customer : null;

            if (cart != null)
            {
                ViewBag.totalPrice = Math.Round(cart.Sum(item => item.Quantity * item.Product.price - item.Product.price * item.discount));
            }
            return View("~/Views/User/Order.cshtml");
        }

        [Route("view")]
        public IActionResult View()
        {
            var customer = SessionHelper.GetObjectFromJson<CustomerSession>(HttpContext.Session, "customer");

            ViewBag.customer = customer != null ? customer : null;

            DatabaseContext databaseContext = new DatabaseContext();
            List<SaleOrderLine> listSaleOrderLines = databaseContext.sale_order_line.ToList();
            List<SaleOrder> listOrders = databaseContext.sale_order.ToList();

            listOrders = listOrders.Where(order => order.customer_id == customer.account_id).ToList();

            List<SaleOrderLine> result = (from a in listOrders
                                          join b in listSaleOrderLines
                                         on a.order_id equals b.order_id
                                         select new SaleOrderLine
                                         (
                                             b.order_id,
                                             b.product_id,
                                             b.quantity,
                                             b.price,
                                             b.discount,
                                             b.amount
                                         )).ToList();

            ViewBag.listOrder = listOrders;
            return View("~/Views/User/Order_View.cshtml");
        }

        [Route("detail")]
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

            return View("~/Views/User/DetailOrder.cshtml");
        }



        [Route("save/")]
        public IActionResult OrderSave()
        {
            DatabaseContext databaseContext = new DatabaseContext();

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            var customer = SessionHelper.GetObjectFromJson<CustomerSession>(HttpContext.Session, "customer");

            if (cart != null && customer !=null)
            {
                int latestOrderId = Order.getLatestOrderId();
                double totalPrice = Math.Round(cart.Sum(item => item.Quantity * item.Product.price - item.Product.price * item.discount));
                databaseContext.sale_order.Add(new SaleOrder(latestOrderId, customer.account_id, DateTime.Now, DateTime.Now, customer.address, totalPrice,1));
                databaseContext.SaveChanges();


                DatabaseContext databaseContextB = new DatabaseContext();

                foreach (var item in cart)
                {
                    var amount = Math.Round(item.Quantity * item.Product.price - item.discount * 100);
                    databaseContextB.sale_order_line.Add(new SaleOrderLine(latestOrderId, item.Product.product_id, item.Quantity, item.Product.price, Convert.ToSingle(item.discount), amount));
                }
                databaseContextB.SaveChanges();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
                this.sendEmail();
            }

            return Redirect("~/");
        }


        [Route("confirm")]
        public IActionResult ConfirmOrder(int order_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var order = databaseContext.sale_order.Where(order => order.order_id == order_id).FirstOrDefault();
            if (order != null)
            {
                order.status = 2;
                databaseContext.SaveChanges();
            }
            return Redirect("~/order/total");

        }

        [Route("cancel")]
        public IActionResult CancelOrder(int order_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var order = databaseContext.sale_order.Where(order => order.order_id == order_id).FirstOrDefault();
            if (order != null)
            {
                order.status = 3;
                databaseContext.SaveChanges();
            }
            return Redirect("~/order/view");
        }

        private void sendEmail()
        {

            var customer = SessionHelper.GetObjectFromJson<CustomerSession>(HttpContext.Session, "customer");

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("User",
            "17520545@gm.uit.edu.vn");

            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(customer.name,
            customer.email);
            message.To.Add(to);

            message.Subject = "This is email subject";

            BodyBuilder bodyBuilder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText("Views/User/Email.cshtml"))
            {
                bodyBuilder.HtmlBody = SourceReader.ReadToEnd();
            }

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("17520545@gm.uit.edu.vn", "1877499254");


            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
