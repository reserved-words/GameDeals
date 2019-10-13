using System.Configuration;
using System.Web.Mvc;

namespace GameDeals.Controllers
{
    public class CallbackController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}