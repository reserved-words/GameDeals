using GameDeals.Core.Interfaces;
using GameDeals.Data.Contracts;
using GameDeals.Data;
using GameDeals.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GameDeals.Api2
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
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = ApiAuthorityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = ApiName;
                });

            services.AddCors();

            services.AddMvcCore()
                .AddMvcOptions(opt => opt.EnableEndpointRouting = false)
                .AddAuthorization();

            services.AddTransient<ILogger, Logger>();
            services.AddTransient(serviceProvider => new Func<IUnitOfWork>(() => new UnitOfWork(ApiConnectionString)));
            services.AddTransient<IMapper, Mapper.Service>();
            services.AddTransient<IRssService, RssService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(
                options => options
                    .WithOrigins(ApiAllowedCorsOrigin)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }

}
