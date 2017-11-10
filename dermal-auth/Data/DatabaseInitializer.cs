using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dermal.auth.Data
{
    public static class DatabaseInitializer
    {
        public static async void InitializeDatabase(IServiceProvider services)
        {
            PerformMigrations(services);
            SeedData(services);
            await SeedUserAsync(services);
        }

        private static void PerformMigrations(IServiceProvider services)
        {
            services.GetRequiredService<DermalAuthDbContext>().Database.Migrate();
            services.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
            services.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        }

        private static async Task SeedUserAsync(IServiceProvider services)
        {
            using (var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
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
            };
        }

        private static void SeedData(IServiceProvider services)
        {
            var context = services.GetRequiredService<ConfigurationDbContext>();
            var config = services.GetRequiredService<IConfiguration>();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
