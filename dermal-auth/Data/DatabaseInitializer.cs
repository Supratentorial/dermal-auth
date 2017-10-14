using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dermal.auth.Data
{
    public static class DatabaseInitializer
    {
        public static async void Initialize(DermalAuthDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) {
            context.Database.EnsureCreated();

            //if (context.Roles.Any(r => r.Name == "Administrator")) return;

            await roleManager.CreateAsync(new IdentityRole("Administrator"));

            string user = "blake.mumford@indoctrinate.com.au";
            string password = "password";
            await userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);
            await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user), "Administrator");
        }
    }
}
