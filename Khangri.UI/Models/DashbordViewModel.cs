using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class HomeViewModel
    {
        
           public List<LocationViewModel> Locations { get; set; } = new List<LocationViewModel>();
           public List<TourViewModel> Tours { get; set; } = new List<TourViewModel>();
           public List<SetupViewModel> Setup { get; set; } = new List<SetupViewModel>();
           public List<CounterViewModel> Counters { get; set; }= new List<CounterViewModel>();
           public List<TourImageViewModel> TourImages { get; set; }=new List<TourImageViewModel>();
           public List<FeatureViewModel> Feature { get; set; }=new List<FeatureViewModel>();
           public List<SliderImageViewModel> SliderImages { get; set; }=new List<SliderImageViewModel> ();
    }
}
