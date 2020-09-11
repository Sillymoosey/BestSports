using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Caching;
using Microsoft.EntityFrameworkCore;
using OutdoorProducts.Models;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.Configuration;

namespace OutdoorProducts
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        private IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<StoreDBContext>(opts =>
            {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:BestSportsConnection"]);
            });
            services.AddScoped<IStoreRepository, EFStoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index" });
                endpoints.MapDefaultControllerRoute();
                });
            SeedData.EnsurePopulated(app);
        }
    }
}
