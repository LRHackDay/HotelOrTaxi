using System.Web.Mvc;

namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        public ActionResult Index(string from, string to)
        {
            return View();
        }
    }
}
