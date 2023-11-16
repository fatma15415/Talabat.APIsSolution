using System.Runtime.CompilerServices;

namespace Talabat.APIs.Extensions
{
    public  static class SwaggerServicesextension
    {
        public static IServiceCollection AddSwaggerservices(this IServiceCollection Services)
        {
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;

        }
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app; 
        }


    }
}
