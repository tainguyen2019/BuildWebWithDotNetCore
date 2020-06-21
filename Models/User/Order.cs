using System;
using System.Linq;
using BuildWebWithDotNetCore.Models.Database;
using BuildWebWithDotNetCore.Models.Home;
namespace BuildWebWithDotNetCore.Models.User
{
    public class Order
    {
        public Order()
        {
        }

        static public int getLatestOrderId()
        {
            try
            {
                DatabaseContext databaseContext = new DatabaseContext();
                var account = databaseContext.sale_order;
                SaleOrder latestOrder = account.OrderByDescending(order => order.order_id).First();

                return latestOrder == null ? 1 : latestOrder.order_id + 1;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            return 1;

        }
    }
}
