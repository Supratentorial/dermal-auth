using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using dermal.auth.Data;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace dermal.auth
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }
        private readonly IHostingEnvironment env;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            this.env = env;
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DermalAuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DermalAuthDb")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DermalAuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(Configuration.GetConnectionString("DermalAuthDb"),
                            db => db.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(Configuration.GetConnectionString("DermalAuthDb"),
                            db => db.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddAspNetIdentity<ApplicationUser>();

            services.AddAuthentication()
               .AddJwtBearer(jwt =>
               {
                   jwt.Authority = "http://localhost:5000";
                   jwt.RequireHttpsMetadata = false;
                   jwt.Audience = "dermal-api";
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DermalAuthDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            DatabaseInitializer.Initialize(context, roleManager, userManager);
        }
    }
}
