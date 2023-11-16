using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
    [Route("errors/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] 
    public class ErrorController : ControllerBase
    {
        //just for redirection
        public ActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(code));  
        }
    }
}
