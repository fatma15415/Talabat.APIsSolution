using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{

    public class Order:BaseEntity
    {
        public Order()
        {
                
        }
        public Order(string buyeremail, Address shippingAddress, DeliveryMethod deliveryMethod
            , ICollection<OrderItem> items, decimal subtotal)
        {
            Buyeremail = buyeremail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string Buyeremail { get; set; }  
        public DateTimeOffset OrderDate { get; set; }   = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public Address ShippingAddress { get; set; }    

        public DeliveryMethod DeliveryMethod { get; set; }
                                                            //Why Hashset ? bcz retrive unique 
        public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>(); 

        public decimal Subtotal { get; set; }
        //[NotMapped]
        //public decimal Total { get; set; }  //Subtotal +DeliveryMethod.cost => derived attr

        // do just function do that

        public decimal GetTotal()
            => Subtotal + DeliveryMethod.Cost;

        public string PaymentIntendId { get; set; } = string.Empty; 

    }
}
