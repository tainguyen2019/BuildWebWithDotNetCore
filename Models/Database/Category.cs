using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class Category
    {
        public int category_id { get; set; }
        public string category_name { get; set; }

        public Category() { }

        public Category(int CategoryID, string CategoryName)
        {
            this.category_id = CategoryID;
            this.category_name = CategoryName;
        }

    }
}
