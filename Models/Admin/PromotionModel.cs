using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Database;
using MySql.Data.MySqlClient;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class PromotionModel
    {
        private string promotion_code;
        private string product_name;
        private DateTime valid_date;
        private DateTime expiry_date;
        private float discount;

        public PromotionModel(string PromotionCode, string ProductName, DateTime ValidDate, DateTime ExpiryDate, float Discount)
        {
            this.discount = Discount;
            this.expiry_date = ExpiryDate;
            this.valid_date = ValidDate;
            this.product_name = ProductName;
            this.promotion_code = PromotionCode;
        }

        public PromotionModel() { }

        public string PromotionCode
        {
            get { return promotion_code; }
            set { promotion_code = value; }
        }
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }

        public DateTime ValidDate
        {
            get { return valid_date; }
            set { valid_date = value; }
        }

        public DateTime ExpiryDate
        {
            get { return expiry_date; }
            set { expiry_date = value; }
        }

        public float Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public List<PromotionModel> getPromotions()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            List<Product> products = databaseContext.product.ToList();
            List<Promotion> promotions = databaseContext.promotion.ToList();
            

            List<PromotionModel> result = (from product in products
                                           join promotion in promotions
                                           on product.product_id equals promotion.product_id
                                           select new PromotionModel(
                                               promotion.promotion_code,
                                               product.product_name,
                                               promotion.valid_date,
                                               promotion.expiry_date,
                                               promotion.discount)).ToList();
            return result;
        }

        public string generatePromotionCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[9];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

    }
}
