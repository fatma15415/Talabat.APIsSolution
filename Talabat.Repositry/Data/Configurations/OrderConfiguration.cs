using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Repositry.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>

    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());  // 1 to 1 total
            builder.Property(O => O.Status).HasConversion(
                OS => OS.ToString(),
                OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));

            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);  
        }

    }
}
