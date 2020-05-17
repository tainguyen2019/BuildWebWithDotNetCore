using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Brand
    {
        public int brand_id { get; set; }
        public string brand_name { get; set; }

        public Brand() { }

        public Brand(int BrandID, string BrandName)
        {
            this.brand_id = BrandID;
            this.brand_name = BrandName;
        }

    }
}
