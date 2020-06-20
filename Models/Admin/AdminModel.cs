using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Admin
{
    public class AdminModel
    {
        private string category_name;
        private double revenue;

        public AdminModel(string CategoryName, double Revenue) {
            this.revenue = Revenue;
            this.category_name = CategoryName;
        }
        public AdminModel() { }

        public string CategoryName {
            get { return category_name; }
            set { category_name = value; }
        }

        public double Revenue {
            get { return revenue; }
            set { revenue = value; }
        }

        public List<Point> getDataChart()
        {
            DatabaseContext database = new DatabaseContext();
            var sale_order = database.sale_order;
            var sale_order_line = database.sale_order_line;
            var product = database.product;
            var category = database.category;

            List<SaleOrder> sale_order_this_month = sale_order.Where(so => so.create_date.Month == DateTime.Now.Month 
                                                             && so.create_date.Year == DateTime.Now.Year
                                                             && so.status == 2).ToList();
            List<AdminModel> list = (from so in sale_order_this_month
                                    join sol in sale_order_line on so.order_id equals sol.order_id
                                    join p in product on sol.product_id equals p.product_id
                                    join c in category on p.category_id equals c.category_id
                                    group new { c.category_name, sol } by new { c.category_name, sol } into data
                                    select new AdminModel
                                    (
                                        data.Key.category_name,
                                        data.Sum(data => data.sol.amount)
                                    )).ToList();
            List<AdminModel> result = new List<AdminModel>();
            foreach(var c in category)
            {
                result.Add(new AdminModel(c.category_name, 0));
            }

            foreach(var l in list)
            {
                foreach(var item in result)
                {
                    if(item.CategoryName == l.CategoryName)
                    {
                        item.Revenue = l.Revenue;
                    }
                }
            }

            List<Point> dataPoints = new List<Point>();
            foreach(var item in result)
            {
                dataPoints.Add(new Point(item.CategoryName, item.Revenue));
            }
            
            return dataPoints;
        }

    }
}
