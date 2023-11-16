using Microsoft.AspNetCore.Http;

namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse:ApiErrorResponse
    {
        public string? Details {get; set;}  
        public ApiExceptionResponse(int Statuscode,string? Message=null, string? Details=null):base(Statuscode, Message)
        {
            this.Details = Details; 

        }
    }
}
