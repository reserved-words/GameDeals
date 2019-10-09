using System.Configuration;
using System.Web.Mvc;

namespace GameDeals.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Deals()
        {
            return PartialView();
        }

        public ActionResult Admin()
        {
            return PartialView();
        }

        public string ApiBaseUrl()
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"];
        }
    }
}