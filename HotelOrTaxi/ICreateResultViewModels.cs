using System;
using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using JourneyCalculator;
using LateRoomsScraper;
using Results;
using TaxiApi.Configuration;
using TaxiApi.Request;
using TaxiApi.Response;
using WebResponse;

namespace HotelOrTaxi
{
    public interface ICreateResultViewModels
    {
        ResultsViewModel Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination);
    }

    public class ResultsViewModelFactory : ICreateResultViewModels
    {
        private readonly ICreateTheTaxiResult _taxiResultFactory;
        private readonly ICreateTheHotelResult _hotelResultFactory;
        private readonly ICanGetTheDistanceOfATaxiJourneyBetweenPoints _distanceCalculator;
        private readonly ICalculateTheWinner _whoIsTheWinner;

        public ResultsViewModelFactory(ICreateTheTaxiResult taxiResultFactory, ICreateTheHotelResult hotelResultFactory,
                                       ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator,
                                       ICalculateTheWinner whoIsTheWinner)
        {
            _taxiResultFactory = taxiResultFactory;
            _hotelResultFactory = hotelResultFactory;
            _distanceCalculator = distanceCalculator;
            _whoIsTheWinner = whoIsTheWinner;
        }

        public ResultsViewModelFactory()
        {
            var canReadConfigurations = new ConfigReader();
            var fareRequestFactory = new FareRequestFactory();
            var webResponseReader = new WebClientWrapper();
            var taxiResultsPage = new CreateTheTaxiControllerUri();
            var performApiRequest = new WebClientApiRequest(canReadConfigurations, webResponseReader);
            var fareResponseFactory = new FareResponseFactory(performApiRequest);
            var taxiFareCalculator = new TaxiFareCalculator(fareRequestFactory, fareResponseFactory);
            var googleMapsDirectionsResponse = new GoogleMapsDirectionsResponse(webResponseReader);
            var googleMapsApiDeserialiser = new GoogleMapsApiDeserialiser();
            var specifyConditionsOfNoTaxiRoutesFound = new SpecifyConditionsOfNoTaxiRoutesFound();
            var hotelStore = new AspNetCache();
            var downloadHtml = new DownloadHtml(webResponseReader);
            var retrieveElementText = new HtmlElement();
            var websiteScraper = new HotelScraper(hotelStore, downloadHtml, retrieveElementText);
            _taxiResultFactory = new TaxiResultFactory(taxiResultsPage, taxiFareCalculator);
            _hotelResultFactory = new HotelResultFactory(websiteScraper);
            _distanceCalculator = new DistanceCalculator(googleMapsDirectionsResponse, googleMapsApiDeserialiser, specifyConditionsOfNoTaxiRoutesFound);
            _whoIsTheWinner = new WhoIsTheWinner();
        }

        public ResultsViewModel Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination)
        {
            Metres distance = null;
            TaxiResult taxi = null;
            HotelResult hotel = null;
            try
            {
                hotel = _hotelResultFactory.Create(startingPoint);
            }
            catch (NoHotelFoundException)
            {
            }

            try
            {
                distance = _distanceCalculator.Calculate(startingPoint, destination);
            }
            catch (NoRouteFoundException)
            {
            }

            Journey journey = null;
            try
            {
                journey = new Journey(startingPoint, distance);
            }
            catch (Exception)
            {
            }

            if (distance != null && journey != null)
            {
                
                try
                {
                    taxi = _taxiResultFactory.Create(urlHelper, journey);
                }
                catch (TaxiApiException)
                {
                }
            }

            if (taxi == null && hotel == null)
            {
                throw new NoClearWinnerExeption();
            }
            return _whoIsTheWinner.Fight(taxi, hotel);
        }
    }
}