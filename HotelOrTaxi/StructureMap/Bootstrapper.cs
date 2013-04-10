using System.Web.Mvc;
using JourneyCalculator;
using LateRoomsScraper;
using Results;
using StructureMap;
using TaxiApi.Request;
using TaxiApi.Response;
using TaxiFirmDetails;
using WebResponse;
using ConfigReader = TaxiFirmDetails.ConfigReader;
using ICanReadConfigurations = TaxiFirmDetails.ICanReadConfigurations;

namespace HotelOrTaxi.StructureMap
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
            ObjectFactory.Initialize(expression =>
                {
                    expression.For<ICreateLocations>().Use<LocationFactory>();
                    expression.For<ICreateResultViewModels>().Use<ResultsViewModelFactory>();
                    expression.For<ICanReadConfigurations>().Use<ConfigReader>();
                    expression.For<IDownloadResponses>().Use<WebClientWrapper>();
                    expression.For<IPerformApiRequest>().Use<WebClientApiRequest>();
                    expression.For<ICreateRequests>().Use<FareRequestFactory>();
                    expression.For<ICreateResponses>().Use<FareResponseFactory>();
                    expression.For<ICalulateTheTaxiFare>().Use<TaxiFareCalculator>();
                    expression.For<ICreateTheTaxiResult>().Use<TaxiResultFactory>();
                    expression.For<IGetTheResponseFromGoogleMapsDirectionsApi>().Use<GoogleMapsDirectionsResponse>();
                    expression.For<IDeserialiseGoogleMapsDirectionsResponses>().Use<GoogleMapsApiDeserialiser>();
                    expression.For<ISaveHotels>().Use<AspNetCache>();
                    expression.For<IDownloadHtml>().Use<DownloadHtml>();
                    expression.For<IRetrieveElementText>().Use<HtmlElement>();
                    expression.For<IScrapeWebsites>().Use<HotelScraper>();
                    expression.For<ICreateTheHotelResult>().Use<HotelResultFactory>();
                    expression.For<ICanGetTheDistanceOfATaxiJourneyBetweenPoints>().Use<DistanceCalculator>();
                    expression.For<ICalculateTheWinner>().Use<WhoIsTheWinner>();
                    expression.For<ICreateTheTaxiControllerUri>().Use<TaxiControllerUriFactory>();
                    expression.For<ISpecifyConditionsOfNoTaxiRoutesFound>().Use<NoTaxiRoutesFoundSpecification>();
                    expression.For<ICreateTaxiViewModels>().Use<TaxiViewModelFactory>();
                    expression.For<IConstructGoogleTextSearchRequests>().Use<GoogleTextSearchRequestConstructor>();
                    expression.For<ICanReadConfigurations>().Use<ConfigReader>();
                    expression.For<IConstructGooglePlaceRequests>().Use<GooglePlaceRequestConstructor>();
                });
        }
    }
}