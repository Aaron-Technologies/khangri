namespace Khangri.Entities
{
    public class EntityTourItinerary
    {
        public int ItineraryId { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public int Day { get; set; } = 1;
        public string Itinery { get; set; }
        public string ItineryDetails { get; set; }
        public int ImageId { get; set; }
        public bool IsActive { get; set; }

    }
}
