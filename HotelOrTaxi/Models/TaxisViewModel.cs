using System.Collections.Generic;
using TaxiFirmDetails;

namespace HotelOrTaxi.Models
{
    public class TaxisViewModel
    {
        public IEnumerable<TaxiFirm> TaxiFirms { get; set; }
    }
}