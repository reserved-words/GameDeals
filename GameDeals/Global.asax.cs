using GameDeals.App_Start;
using GameDeals.Core.Interfaces;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GameDeals
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Removes MVC version info from HTTP response header
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
        }

        //protected void Application_Error()
        //{
        //    var exception = Server.GetLastError();
        //    var logger = DependencyResolver.Current.GetService<ILogger>();
            
        //    if (logger != null)
        //    {
        //        logger.Log(exception);
        //    }

        //    Response.Clear();
        //    Server.ClearError();
        //    Response.Redirect(string.Format("~/Error/Index"));
        //}
    }
}
