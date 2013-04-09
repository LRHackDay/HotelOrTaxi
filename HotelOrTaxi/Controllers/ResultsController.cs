using System;
using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;

namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ICreateResultViewModels _resultsViewModelFactory;
        private readonly ICreateLocations _createLocations;

        public ResultsController(ICreateResultViewModels resultsViewModelFactory, ICreateLocations createLocations)
        {
            _resultsViewModelFactory = resultsViewModelFactory;
            _createLocations = createLocations;
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var resultsViewModel = new ResultsViewModel();
            try
            {
                var startingPoint = new StartingPoint(_createLocations.GetLocation(fromlatlong), from);
                var destination = new Destination(_createLocations.GetLocation(tolatlong), to);
                
                resultsViewModel = _resultsViewModelFactory.Create(Url, startingPoint, destination);

                return View("Index", resultsViewModel);
            }
            catch (Exception e)
            {
                resultsViewModel.Error = e;
            }

            return View("Error", resultsViewModel);
        }

        public ViewResult Fight()
        {
            return View();
        }
    }

    public interface ICreateLocations
    {
        Location GetLocation(string latlong);
    }

    public class LocationFactory : ICreateLocations
    {
        public Location GetLocation(string latlong)
        {
            if (latlong != null && latlong.Contains(","))
            {
                string[] strings = latlong.Split(',');

                var latitude = new Latitude(strings[0]);
                var longitude = new Longitude(strings[1]);

                return new Location(latitude, longitude);
            }
            return null;
        }
    }
}