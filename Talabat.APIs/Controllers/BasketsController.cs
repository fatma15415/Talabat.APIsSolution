using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositry;

namespace Talabat.APIs.Controllers
{
   
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepositry _basketRepo;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepositry basketRepo , IMapper mapper)
        {
           _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet] //Get: /api/baskets/not id
        // return collection of products that in customer basket
        public async Task<ActionResult<CustomerBasket>> Getcustomerbasket(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);

            return basket is null ? new CustomerBasket(id) : basket;    
        }
        [HttpPost]  //Post :  /api/baskets
        public async Task<ActionResult<CustomerBasketDTO>> UpdateBasket(CustomerBasketDTO  basket)
        {
            var mappedbasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
            var CreatedOrUpdated= await _basketRepo.UpdateBasketAsync(mappedbasket);
            if (CreatedOrUpdated is null) return BadRequest(new ApiErrorResponse(400));
            return  Ok(CreatedOrUpdated);   

        }
        [HttpDelete] // delete :api/Baskets  // >> take it as query string
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepo.DeleteBasketAsync(id);

        }

    }
}
