using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Promotion
    {
        public string promotion_code { get; set; }
        public int product_id { get; set; }
        public float discount { get; set; }
        public DateTime valid_date { get; set; }
        public DateTime expiry_date { get; set; }

        public Promotion() { }

        public Promotion(string PromotionCode, int ProductID, float Discount, DateTime ValidDate, DateTime ExpiryDate)
        {
            this.promotion_code = PromotionCode;
            this.product_id = ProductID;
            this.discount = Discount;
            this.valid_date = ValidDate;
            this.expiry_date = ExpiryDate;
        }

    }
}
