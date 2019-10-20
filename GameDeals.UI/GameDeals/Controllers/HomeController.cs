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

        public ActionResult Login()
        {
            return PartialView();
        }

        public ActionResult Deals()
        {
            return PartialView();
        }

        public ActionResult Admin()
        {
            return PartialView();
        }

        public ActionResult Callback()
        {
            return View();
        }

        public string ApiBaseUrl()
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"];
        }
    }
}