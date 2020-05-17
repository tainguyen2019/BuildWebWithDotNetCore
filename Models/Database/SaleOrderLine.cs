using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class SaleOrderLine
    {
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public float discount { get; set; }
        public double amount { get; set; }

        public SaleOrderLine() { }

        public SaleOrderLine(int OrderID, int ProductID, int Quantity, double Price, float Discount, double Amount)
        {
            this.order_id = OrderID;
            this.product_id = ProductID;
            this.quantity = Quantity;
            this.price = Price;
            this.discount = Discount;
            this.amount = Amount;
        }

    }
}
