using AspNetCore.Entities;
using AspNetCore.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Routing;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace AspNetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<RestaurantDbContext>(options => 
                    options.UseSqlServer(Configuration["database:connection"]));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RestaurantDbContext>();

            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment,IApplicationEnvironment appEnvironment,
            IGreeter greeter)
        {
            app.UseIISPlatformHandler();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // ...
            }
            
            app.UseRuntimeInfoPage("/info");
            
            app.UseFileServer();

            app.UseNodeModules(appEnvironment);

            app.UseIdentity();

            app.UseMvc(ConfigureRoutes);

            app.Run(async (context) =>
            {
                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
