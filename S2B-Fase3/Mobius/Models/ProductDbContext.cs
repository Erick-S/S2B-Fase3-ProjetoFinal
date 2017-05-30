using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

// https://stackoverflow.com/questions/28531201/entitytype-identityuserlogin-has-no-key-defined-define-the-key-for-this-entit Trying this to fix issue

namespace Mobius.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext() : base("ProductDbContext")
        {
            // remove default initializer
            Database.SetInitializer<ProductDbContext>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public static ProductDbContext Create()
        {
            return new ProductDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}