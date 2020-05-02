using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Category
    {
        private int id { get; set; }
        private string loaisp { get; set; }

        public Category() { }

        public Category(int ID, string LoaiSP)
        {
            id = ID;
            loaisp = LoaiSP;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string LoaiSP
        {
            get { return loaisp; }
            set { loaisp = value; }
        }
    }
}
