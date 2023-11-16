using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities;
using Talabat.Core.Repositry;
using Talabat.Core.Specification;

namespace Talabat.APIs.Controllers
{
  
    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepositry<Product> _ProductRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepositry<ProductBrand> _brandRepo;
        private readonly IGenericRepositry<ProductType> _typeRepo;

        public ProductsController(IGenericRepositry<Product> ProductRepo , IMapper mapper ,
                                  IGenericRepositry<ProductBrand> brandRepo,IGenericRepositry<ProductType> typeRepo)
        {
            _ProductRepo = ProductRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
        }
       // [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> Getproducts([FromQuery]ProductSpecParam productSpec)
        {
            var spec=new ProductSpecification(productSpec);  
            var Products= await _ProductRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList< Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var CountSpec = new ProductWithFilterationForCountSpec(productSpec);
            var count = await _ProductRepo.GetCountWithSpecAsync(CountSpec);
            // Return response AS Data
            return Ok(new Pagination<ProductToReturnDto>(productSpec.PageIndex, productSpec.PageSize,count, data));    
        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)

        {
            var spec = new ProductSpecification(id);
            var product = await _ProductRepo.GetByIDlWithSpecAsync(spec);
            if (product is null) return NotFound(new ApiErrorResponse(404));
            var Mappedproduct=_mapper.Map<Product, ProductToReturnDto>(product);  
            
            return Ok(Mappedproduct);

        }


        [HttpGet("Brands")] //api/products/Brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands= await _brandRepo.GetAllAsync();

            return Ok(Brands);
        }
        [HttpGet("Types")] //api/products/Types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Types = await _typeRepo.GetAllAsync();

            return Ok(Types);
        }

    }
}
