using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using INDG.GRIP.Trader.Presentation;
using INDG.GRIP.Trader.Bootstrapper.Extensions;
using INDG.GRIP.Trader.Application;
using INDG.GRIP.Trader.Persistence;
using INDG.GRIP.Trader.Application.Logic.Products.GetProducts;

namespace INDG.GRIP.Trader.Bootstrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services
                .AddPresentation(Configuration)
                .AddSwagger()
                .AddAutoMapper(
                    Assembly.GetAssembly(typeof(GetProductsQuery)),
                    Assembly.GetAssembly(typeof(Startup)))
                .AddVersionConfiguration()
                .AddOptionsExtensions(Configuration)
                .AddMemoryCache()
                .AddAuthentication(Configuration)
                .AddApplication()
                .AddPersistenceData(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.SwaggerConfiguration();

            app.UseHttpsRedirection();


            app.UseRouting();
            app.UseStaticFiles();

            app.PresentationMap();
            env.AutoMapperConfigure();
        }
    }
}
