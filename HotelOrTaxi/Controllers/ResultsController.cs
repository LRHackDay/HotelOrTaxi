using System.Web.Mvc;
using HotelOrTaxi.Models;
using LateRoomsScraper;

namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var scraperResponse = new HotelScraper(from).Scrape();

            var hotels = ((ScraperResponse)scraperResponse).Hotels;

            return View("Index", new ResultsViewModel());
        }
    }
}