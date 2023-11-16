using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int deliverymethodId, Address Shippingaddress);
        Task<IReadOnlyList<Order>> CreateOrdersForUserAsync(string BuyerEmail);
        Task<Order> CreateOrderByIdForUserAsync(int OrderId, string BuyerEmail);
    }
}
