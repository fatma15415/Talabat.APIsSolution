using Microsoft.AspNetCore.Http;

namespace Talabat.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; } 
        public string? Message { get; set; }

        public ApiErrorResponse(int _statuscode, string? _message = null)
        {
            StatusCode = _statuscode;   
            Message = _message ?? GetDefaultMessageForSatutsCode(_statuscode); 
                
        }

        private string? GetDefaultMessageForSatutsCode(int _statuscode)
        {
            return _statuscode switch
            {
                400 => "A Bad request , YOU HAVE MADE",
                401 => "Authorize, You Are Not",
                404 => " Resourses not found",
                500 => "there is server error",
                _ => null
            };

        }





    }
}
