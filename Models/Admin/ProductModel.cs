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
    }
}
