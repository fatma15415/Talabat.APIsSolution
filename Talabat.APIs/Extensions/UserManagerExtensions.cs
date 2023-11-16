using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal Currentuser)
        {
            var email= Currentuser.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);

            return user;

        }

    }
}
