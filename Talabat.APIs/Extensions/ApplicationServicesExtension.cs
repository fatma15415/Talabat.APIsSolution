using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repositry;
using Talabat.Core.Services;
using Talabat.Repositry;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationservicesextension(this IServiceCollection Services)
        {
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IUnitOfWork,UnitOfWork>();   
           // Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            Services.AddAutoMapper(typeof(MappingProfiles));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var error = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                      .SelectMany(P => P.Value.Errors)
                                                      .Select(E => E.ErrorMessage).ToArray();

                    var Validationerrorresponse = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(Validationerrorresponse);
                };
            }
            );

            Services.AddScoped(typeof(IBasketRepositry), typeof(BasketRepositry));

            return Services;

        }
    }
}
