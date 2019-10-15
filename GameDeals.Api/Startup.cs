using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup("Startup", typeof(GameDeals.Api.Startup))]
namespace GameDeals.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = ConfigurationManager.AppSettings["AuthUrl"],
                RequiredScopes = new[] { ConfigurationManager.AppSettings["AuthScope"] },
            });

            var config = GlobalConfiguration.Configuration;
            config.Filters.Add(new AuthorizeAttribute());
            app.UseWebApi(config);
        }
    }
}