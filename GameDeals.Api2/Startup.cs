using GameDeals.Core.Interfaces;
using GameDeals.Data.Contracts;
using GameDeals.Data2;
using GameDeals.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GameDeals.Api2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "GameDealsApi";
                });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("default", policy =>
            //    {
            //        policy.WithOrigins("http://localhost:50606")
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //    });
            //});

            services.AddCors();

            services.AddMvcCore()
                .AddMvcOptions(opt => opt.EnableEndpointRouting = false)
                .AddAuthorization();

            services.AddTransient<ILogger, Logger>();
            services.AddTransient(serviceProvider => new Func<IUnitOfWork>(() => new UnitOfWork("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GameDeals;Integrated Security=True;", "dbo")));
            services.AddTransient<IMapper, Mapper.Service>();
            services.AddTransient<IRssService, RssService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(
                options => options
                    .WithOrigins("http://localhost:50606")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }

}
