using HotelOrTaxi.Models;

namespace HotelOrTaxi
{
    public interface ICreateTaxiViewModels
    {
        TaxisViewModel Create();
    }

    public class TaxiViewModelFactory : ICreateTaxiViewModels
    {
        public TaxisViewModel Create()
        {
            return new TaxisViewModel();
        }
    }
}