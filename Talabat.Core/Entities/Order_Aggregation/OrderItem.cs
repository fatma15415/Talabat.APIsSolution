using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    //this is added to DB as table
    //OI ==> POI 1 to 1 Total
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
                
        }
        public OrderItem(ProductOrderItem product, decimal price, int quantity)
        {
            this.product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductOrderItem product { get; set; }
        public decimal Price { get; set; }  
        public int Quantity { get; set; }   


    }
}
