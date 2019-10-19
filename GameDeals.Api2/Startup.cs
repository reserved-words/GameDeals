using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseCors("default");
            //app.UseMvc();

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
