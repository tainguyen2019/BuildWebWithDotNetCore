using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<Account> getAccount()
        {
            MySqlConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "select email, password from account";
            MySqlCommand query = new MySqlCommand();
            query.CommandText = sql;
            query.Connection = conn;
            MySqlDataReader reader = query.ExecuteReader();
            List<Account> data = new List<Account>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data.Add(new Account(reader.GetString(0), reader.GetString(1)));
                }
            }
            
            return data;
        }

        public List<Product> getProduct()
        {
            MySqlConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "SELECT * from product";
            MySqlCommand query = new MySqlCommand();
            query.CommandText = sql;
            query.Connection = conn;
            MySqlDataReader reader = query.ExecuteReader();
            List<Product> data = new List<Product>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data.Add(new Product(reader.GetInt16(0), reader.GetString(1), reader.GetInt16(2), reader.GetInt16(4) , reader.GetInt64(5)));
                }
            }

            return data;
        }

        public List<Category> getCategory()
        {
            MySqlConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "select * from category";
            MySqlCommand query = new MySqlCommand();
            query.CommandText = sql;
            query.Connection = conn;
            MySqlDataReader reader = query.ExecuteReader();
            List<Category> data = new List<Category>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data.Add(new Category(reader.GetInt16(0), reader.GetString(1)));
                }
            }

            return data;
        }
    }
}
