namespace Results
{
    public class HotelResultFactory : ICreateTheHotelResult
    {
        public Result Create()
        {
            var hotel = new Result();
            hotel.Name = "Hotel";
            hotel.Price = 30.00;
            hotel.Uri =
                "http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=Manchester&d=20130404&n=1&adults=1&children=0&minp=&maxp=&StarRatingFilter=&rt=1-0&Latitude=&Longitude=&MaxRadius=1&PageSize=10&toStep=&SortBy=Price&Ascending=True&findButton=FIND+HOTELS&sb=tp&sd=true";
            return hotel;
        }
    }
}