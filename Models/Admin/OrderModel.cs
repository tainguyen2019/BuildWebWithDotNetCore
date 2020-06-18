using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Admin
{
    public class OrderModel
    {
        private string product_name;
        private int quantity;
        private double price;
        private float discount;
        private double amount;

        public OrderModel(string ProductName, int Quantity, double Price, float Discount, double Amount)
        {
            product_name = ProductName;
            price = Price;
            quantity = Quantity;
            discount = Discount;
            amount = Amount;
        }

        public OrderModel() { }

        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public float Discount
        {
            get { return discount; }
            set { discount = value; }
        }
    }
}
