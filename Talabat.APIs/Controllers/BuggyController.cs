using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repositry.Data;

namespace Talabat.APIs.Controllers
{
    
    public class BuggyController :ApiBaseController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        [HttpGet("NotFound")]//api/buggy/notfound
        public ActionResult GetNotFoundRequest()
        {
            var product = _storeContext.products.Find(100);
            if(product is null) return NotFound(new ApiErrorResponse(404));
            return Ok(product);
        }
        [HttpGet("ServerError")]//api/buggy/ServerError

        public ActionResult GetServerError()
        {
            var product = _storeContext.products.Find(100);
            var producttoreturn = product.ToString();  //Will THrow exception
            return Ok(producttoreturn);
        }
        [HttpGet("BadRequest")]//api/buggy/BadRequest

        public ActionResult GetBadRequest()
        { 
            
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("BadRequest/{id}")]//api/buggy/BadRequest/five
        public ActionResult GetBadRequest(int id)
        {

            return Ok();
        }
 
    }
}
