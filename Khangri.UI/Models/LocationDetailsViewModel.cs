using Khangri.Entities;

namespace Khangri.UI.Models
{
    public class LocationDetailsViewModel
    {
        public LocationViewModel Location { get; set; }=new LocationViewModel();
        public List<EntitySightSeeing> SightSeeing { get; set;}=new List<EntitySightSeeing>();
        public List<EntityLocationImage> Image { get; set; } = new List<EntityLocationImage>();
        public List<ContactViewModel> Contacts { get; set; }= new List<ContactViewModel>();
    }
}
