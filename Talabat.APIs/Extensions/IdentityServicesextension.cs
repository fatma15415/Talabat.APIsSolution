using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Repositry.Identity;
using Talabat.APIs.Extensions;
using Talabat.Core.Services;
using Talabat.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Talabat.APIs.Extensions
{
    public static class IdentityServicesextension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;    
                option.Password.RequireLowercase = true;    
                option.Password.RequireUppercase = true;    
                option.Password.RequireNonAlphanumeric = true;


            }).AddEntityFrameworkStores<AppIdentityDBContext>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme; 
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer= true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateAudience= true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateLifetime= true, 
                    ValidateIssuerSigningKey= true, 
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]))
            };
            }
                
                );
            return services;    
        }

    }
}
