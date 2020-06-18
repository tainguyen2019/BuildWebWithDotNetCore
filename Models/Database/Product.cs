using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Product
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int category_id { get; set; }
        public int brand_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string image { get; set; }
        public int status { get; set; }
        public string description { get; set; }


        public Product() { }

        public Product(int ProductID, string ProductName, int CategoryID, int BrandID, 
                        int Quantity, double Price, string Image, int Status, string Description)
        {
            this.product_id = ProductID;
            this.product_name = ProductName;
            this.category_id = CategoryID;
            this.brand_id = BrandID;
            this.quantity = Quantity;
            this.price = Price;
            this.image = Image;
            this.status = Status;
            this.description = Description;
        }
    }
}
