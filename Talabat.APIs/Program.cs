using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositry;
using Talabat.Repositry;
using Talabat.Repositry.Data;
using Talabat.Repositry.Data.DataSeed;
using Talabat.Repositry.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            


            // Add services to the container.
            //------------Databases------------------------------
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreContext>(option => {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);

            });

            builder.Services.AddDbContext<AppIdentityDBContext>(option => {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });
            //-------------------------------------------------------
            //------------------Extension services-------------------
            builder.Services.AddApplicationservicesextension();

            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddSwaggerservices();  
            
            var app = builder.Build();
            // Explicitly
            #region Update Database inside Main

            var scope = app.Services.CreateScope();    // to talk all scopped services
            var services = scope.ServiceProvider;        // DI   to resolve dependncies 
                                                         //Logger Factory
            var Loggerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetRequiredService<StoreContext>();    //ASk CLR to create object from store context explicitly
                await dbcontext.Database.MigrateAsync();  //Update Database
                await StoreContextSeed.SeedAsync(dbcontext);  
                var IdentityDbContext = services.GetRequiredService<AppIdentityDBContext>();    
                await IdentityDbContext.Database.MigrateAsync();  //Update Database  
                var usermanager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(usermanager);   
            }
            catch (Exception ex)
            {
                var Logger = Loggerfactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured during Applaying Migration");
            }

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseMiddleware<ExceptionMiddlewares>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            
            app.UseAuthentication();    
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}