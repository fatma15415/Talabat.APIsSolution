using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Core.Specification.OrderSpec
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string BuyerEmail):
            base(o=>o.Buyeremail == BuyerEmail) 
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDescebding(o => o.OrderDate);
        }

    }
}
