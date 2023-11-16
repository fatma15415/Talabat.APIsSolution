using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repositry.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(! userManager.Users.Any()) 
            {
                var user = new AppUser()
                {
                    DisplayName="Fatma Gamal",
                    Email="fatma.gamal.gmail.com",
                    UserName= "fatma.gamal",
                    PhoneNumber="01331732001",
                };
                await userManager.CreateAsync(user, "Pp@ssw0rd");
            }
        }
    }
}
