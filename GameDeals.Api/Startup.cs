using GameDeals.Core.Interfaces;
using GameDeals.Data.Contracts;
using GameDeals.Data;
using GameDeals.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GameDeals.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public string ApiAllowedCorsOrigin => _config.GetValue<string>("ApiAllowedCorsOrigin");
        public string ApiAuthorityUrl => _config.GetValue<string>("ApiAuthorityUrl");
        public string ApiConnectionString => _config.GetValue<string>("ApiConnectionString");
        public string ApiName => _config.GetValue<string>("ApiName");

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0.0", new OpenApiInfo { Title = "GameDealsAPI", Version = "1.0.0" });
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = ApiAuthorityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = ApiName;
                });

            services.AddCors();

            services.AddMvcCore()
                 .AddApiExplorer()
                .AddMvcOptions(opt => opt.EnableEndpointRouting = false)
                .AddAuthorization();

            services.AddTransient<ILogger, Logger>();
            services.AddTransient(serviceProvider => new Func<IUnitOfWork>(() => new UnitOfWork(ApiConnectionString)));
            services.AddTransient<IMapper, Mapper.Service>();
            services.AddTransient<IRssService, RssService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            app.UseCors(
                options => options
                    .WithOrigins(ApiAllowedCorsOrigin)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "GameDealsAPI 1.0.0");
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }

}
