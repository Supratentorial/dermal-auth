using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using dermal.auth.Data;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using dermal.auth.Interfaces;
using dermal.auth.Services;
using dermal.auth.Options;

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
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DermalAuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalDb")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DermalAuthDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddMvc();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(Configuration.GetConnectionString("LocalDb"),
                            db => db.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(Configuration.GetConnectionString("LocalDb"),
                            db => db.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddAspNetIdentity<ApplicationUser>();


            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmsSender, SmsSender>();
            services.AddTransient<IUserMapper, UserMapper>();

            services.Configure<SendGridOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
            builder.WithOrigins(Configuration["AngularClientHost"])
            .AllowAnyHeader()
            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }


}


