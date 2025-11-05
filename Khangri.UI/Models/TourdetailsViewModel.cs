namespace Khangri.UI.Models
{
    public class TourdetailsViewModel
    {

        public TourViewModel Tours { get; set; }=new TourViewModel();
        public List<TourImageViewModel>TourImages { get; set; } = new List<TourImageViewModel>();
        public List<TourItineraryViewModel> TourItinerary { get; set; } = new List<TourItineraryViewModel>();
        public TourTermsConditionViewModel TourTermsCondition { get; set; }= new TourTermsConditionViewModel();
        public List<ContactViewModel> Contacts { get; set; }   =new List<ContactViewModel>();
    }
}
