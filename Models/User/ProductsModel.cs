using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.User
{
    public class ProductsModel
    {
        public ProductsModel() { }
        public List<Product> showProducts(int category_id,int brand_id)
        {
           DatabaseContext databaseContext = new DatabaseContext();
                var products = databaseContext.product;
            List<Product> result = products
                                .Where(product => product.category_id == category_id)
                                .ToList();

            if (brand_id != 0)
            {
                result = result.Where(product => product.brand_id == brand_id).ToList();
            }
                
                return result;
        }
        public Product showProductDetail(int product_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var product = databaseContext.product;
            Product result = product
                            .Where(product => product.product_id == product_id).First();
            return result;
        }   
        public Product searchProduct(string product_name)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var product = databaseContext.product;
            Product result = product
                            .Where(product => product.product_name.Contains(product_name)).First();
            return result;
        }
        public int count_products(int category_id, int brand_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var products = databaseContext.product;
            var brands = databaseContext.brand;
            int result = (from product in products
                          join brand in brands
                          on product.brand_id equals brand.brand_id
                          where product.category_id == category_id
                          where brand.brand_id == brand_id
                          select product).Count();
            return result;
        }

        public List<Brand> showBrands(int category_id)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var categories = databaseContext.category;
            var brands = databaseContext.brand;
            var cate_brands = databaseContext.category_brand;

            List<Brand> result = (from brand in brands
                                  join catebrand in cate_brands 
                                  on brand.brand_id equals catebrand.brand_id
                                  where catebrand.category_id == category_id
                                  select new Brand(brand.brand_id, brand.brand_name)).ToList();
            return result;
        }
    }
}
