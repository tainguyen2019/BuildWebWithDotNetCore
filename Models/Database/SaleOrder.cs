using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class SaleOrder
    {
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public DateTime create_date { get; set; }
        public DateTime write_date { get; set; }
        public string address { get; set; }
        public double total { get; set; }
        public int status { get; set; }

        public SaleOrder() { }

        public SaleOrder(int OrderID, int CustomerID, DateTime CreateDate, DateTime WriteDate
                          , string Address, double Total, int Status)
        {
            this.order_id = OrderID;
            this.customer_id = CustomerID;
            this.create_date = CreateDate;
            this.write_date = WriteDate;
            this.address = Address;
            this.total = Total;
            this.status = Status;
        }

    }
}
