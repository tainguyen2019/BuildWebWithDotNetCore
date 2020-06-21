using System;
using System.Collections.Generic;
using System.Linq;
using BuildWebWithDotNetCore.Models.Database;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Home;

namespace BuildWebWithDotNetCore.Models.User
{
    public class HomeModel
    {
        public HomeModel() { }
        public List<Product> getProductbyCategory(int category_id)
        {
            try
            {
                DatabaseContext databaseContext = new DatabaseContext();
                var products = databaseContext.product;
                List<Product> result = products
                                .Where(product => product.category_id == category_id)
                                .Take(4)
                                .ToList();
                return result;
            }

            // Catch whatever exception you expect to raise
            // and/or do any necessary cleanup in a finally block
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
           
        }
    }
}
