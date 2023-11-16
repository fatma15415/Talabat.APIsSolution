using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Repositry.Data.Configurations;
using Order = Talabat.Core.Entities.Order_Aggregation.Order;

namespace Talabat.Repositry.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options ):base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConiguration());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());   
            //modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());    
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }  
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> ordersItems { get; set; }   
        public DbSet<DeliveryMethod> deliveryMethods { get; set; }  




}
}
