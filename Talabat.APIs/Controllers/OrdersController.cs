using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]

        [HttpPost] //POST: api/orders
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var mappedaddress = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDTO.BasketId,
                                                           orderDTO.DeliveryMethodId, mappedaddress);

            if (order is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.CreateOrdersForUserAsync(BuyerEmail);

            return Ok(orders);  
        }
    }
}
