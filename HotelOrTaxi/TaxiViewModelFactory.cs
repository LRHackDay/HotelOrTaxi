using System.Collections.Generic;
using Geography;
using HotelOrTaxi.Models;
using TaxiFirmDetails;

namespace HotelOrTaxi
{
    public interface ICreateTaxiViewModels
    {
        TaxisViewModel Create(Location longitude);
    }

    public class TaxiViewModelFactory : ICreateTaxiViewModels
    {
        private readonly TaxiFirmFactory _taxiFirmFactory;

        public TaxiViewModelFactory(TaxiFirmFactory taxiFirmFactory)
        {
            _taxiFirmFactory = taxiFirmFactory;
        }

        public TaxisViewModel Create(Location location)
        {
            var taxisViewModel = new TaxisViewModel
                {
                    TaxiFirms = _taxiFirmFactory.Create(location)
                };

            return taxisViewModel;
        }
    }
}