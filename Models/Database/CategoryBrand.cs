using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class CategoryBrand
    {
        public int category_id { get; set; }
        public int brand_id{ get; set; }

        public CategoryBrand() { }

        public CategoryBrand(int CategoryID, int BrandID)
        {
            this.category_id = CategoryID;
            this.brand_id = BrandID;
        }

    }
}
