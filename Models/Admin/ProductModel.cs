using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Database;
using MySql.Data.MySqlClient;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class ProductModel
    {
        private int id;
        private string tensp;
        private string loaisp;
        private int soluong;
        private double gia ;

        public ProductModel(int ID, string TenSP, string LoaiSP, int SoLuong, double Gia)
        {
            id = ID;
            tensp = TenSP;
            loaisp = LoaiSP;
            soluong = SoLuong;
            gia = Gia;
        }

        public ProductModel() { }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string TenSP
        {
            get { return tensp; }
            set { tensp = value; }
        }

        public string LoaiSP
        {
            get { return loaisp; }
            set { loaisp = value; }
        }

        public int SoLuong
        {
            get { return soluong; }
            set { soluong = value; }
        }

        public double Gia
        {
            get { return gia; }
            set { gia = value; }
        }

        public List<ProductModel> getProducts(int category_id, string query)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            List<Product> products = databaseContext.product.ToList();
            List<Category> categories = databaseContext.category.ToList();

            if (category_id != 0)
            {
                products = products.Where(product => product.category_id == category_id).ToList();
            }

            if (query.Length > 0)
            {
                products = products.Where(product => product.product_name.Contains(query)).ToList();
            }

            List<ProductModel> result = (from product in products
                                         join category in categories
                                         on product.category_id equals category.category_id
                                         orderby product.product_id descending
                                         select new ProductModel
                                         (
                                             product.product_id,
                                             product.product_name,
                                             category.category_name,
                                             product.quantity,
                                             product.price
                                         )).ToList();
            return result;
        }

    }
}
