using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositry;
using Talabat.Core.Services;
using Talabat.Core.Specification.OrderSpec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepositry _basketRepositry;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepositry<Product> _ProductRepo;
        //private readonly IGenericRepositry<DeliveryMethod> _dMRepo;
        //private readonly IGenericRepositry<Order> _orderRepo;

        public OrderService(IBasketRepositry basketRepositry,IUnitOfWork unitOfWork)
        {
            _basketRepositry = basketRepositry;
            _unitOfWork = unitOfWork;
            //_ProductRepo = ProductRepo;
            //_dMRepo = DMRepo;
            //_orderRepo = orderRepo;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int deliverymethodId, Address Shippingaddress)
        {
            //1.Get Basket from basket Repo
            var Basket = await _basketRepositry.GetBasketAsync(BasketId);
            //2.Get Selected Item at basket from productRepo
            var OrderItems = new List<OrderItem>();

            if(Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repositry<Product>().GetByIDAsync(item.Id);
                    var productitemsordered = new ProductOrderItem(product.Id, product.Name, product.PictureUrl);
                    var orderitem = new OrderItem(productitemsordered, product.Price, item.Quantity);

                    OrderItems.Add(orderitem);  
                }
            }
            //3. Calculate subtotal
            var Subtotal = OrderItems.Sum(item => item.Price * item.Quantity);

            //4.Get Delivery Method from DM Repositry
            var deliverymethod = await _unitOfWork.Repositry<DeliveryMethod>().GetByIDAsync(deliverymethodId);
            //5. Create Order
            var order = new Order(BuyerEmail, Shippingaddress, deliverymethod, OrderItems, Subtotal);
            //6. AddOrder Locally
            await _unitOfWork.Repositry<Order>().Add(order); // Local
            //7.Save order to DataBase (order)
             var result =await _unitOfWork.Complete();
            if (result <= 0) return null;

            return order;
        }

        public Task<Order> CreateOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> CreateOrdersForUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpecification(BuyerEmail);
            var orders = await _unitOfWork.Repositry<Order>().GetAllWithSpecAsync(spec);
            return orders;  
        }
    }
}
