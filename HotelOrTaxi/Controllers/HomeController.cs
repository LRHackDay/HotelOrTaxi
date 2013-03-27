using System.Web.Mvc;

namespace HotelOrTaxi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
