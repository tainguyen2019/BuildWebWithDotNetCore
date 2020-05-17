using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildWebWithDotNetCore.Models.Home;
using Microsoft.EntityFrameworkCore;

namespace BuildWebWithDotNetCore.Models.Database
{
    public class DatabaseContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=website;uid=root;password=";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(account => new
            {
                account.account_id
            });

            modelBuilder.Entity<Product>().HasKey(product => new
            {
                product.product_id
            });

            modelBuilder.Entity<Category>().HasKey(category => new
            {
                category.category_id
            });
            modelBuilder.Entity<Brand>().HasKey(brand => new
            {
                brand.brand_id
            });
            modelBuilder.Entity<CategoryBrand>().HasKey(categorybrand => new
            {
                categorybrand.category_id,
                categorybrand.brand_id
            });
            modelBuilder.Entity<Promotion>().HasKey(promotion => new
            {
                promotion.promotion_code
            });
            modelBuilder.Entity<Customer>().HasKey(customer => new
            {
                customer.customer_id
            });
            modelBuilder.Entity<SaleOrder>().HasKey(sale_order => new
            {
                sale_order.order_id
            });
            modelBuilder.Entity<SaleOrderLine>().HasKey(sale_order_line => new
            {
                sale_order_line.order_id,
                sale_order_line.product_id
            });
        }

        public DbSet<Account> account { set; get; }
        public DbSet<Product> product { set; get; }
        public DbSet<Category> category { set; get; }
        public DbSet<Brand> brand { set; get; }
        public DbSet<CategoryBrand> category_brand { set; get; }
        public DbSet<Promotion> promotion { set; get; }
        public DbSet<Customer> customer { set; get; }
        public DbSet<SaleOrder> sale_order { set; get; }
        public DbSet<SaleOrderLine> sale_order_line { set; get; }
    }
}
