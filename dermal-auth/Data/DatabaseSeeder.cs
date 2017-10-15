using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace dermal.auth.Data
{


    public static class DatabaseSeeder
    {
        public static async void Seed(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DermalAuthDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                if (context.Roles.Any(r => r.Name == "Administrator"))
                {
                    return;
                };

                await roleManager.CreateAsync(new IdentityRole("Administrator"));

                string user = "blake.mumford@indoctrinate.com.au";
                string password = "password";
                await userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user), "Administrator");
            }
        }
    }
}