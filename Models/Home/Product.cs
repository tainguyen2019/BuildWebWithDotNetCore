using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Product
    {
        private int id{ get; set; }
        private string tensp{ get; set; }
        private int loaisp{ get; set; }
        private int soluong{ get; set; }
        private double gia { get; set; }

        public Product() { }

        public Product(int ID, string TenSP, int LoaiSP, int Soluong , double Gia)
        {
            id = ID;
            tensp = TenSP;
            loaisp = LoaiSP;
            soluong = Soluong;
            gia = Gia;
        }

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

        public int LoaiSP
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
