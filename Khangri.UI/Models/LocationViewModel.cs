using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class LocationViewModel
    {
        
            public int LocationId { get; set; }
            [Required(ErrorMessage = "Please Enter an Location ")]
            public string Location { get; set; }
            [Required(ErrorMessage = "Please Enter an Caption ")]
            public string Caption { get; set; }
          //  [Required(ErrorMessage ="Please Select an Parent Location")]
            public int M01_LocationId { get; set; }
            public string ParentLocation { get; set; }
            [Required(ErrorMessage = "Please Enter an Description ")]
            public string Description { get; set; }
            [Required(ErrorMessage = "Please Enter an Altitude ")]
            public string Altitude { get; set; }
            [Required(ErrorMessage = "Please Enter an ImageFiles ")]
             public string ImageFiles { get; set; }
            // [Required(ErrorMessage = "Please Enter an WeatherUrl ")]
            public string WeatherUrl { get; set; }
            // [Required(ErrorMessage = "Please Enter an GoogleMapUrl ")]
            public string GoogleMapUrl { get; set; }
            // [Required(ErrorMessage = "Please Enter an TripAdvisorUrl ")]
            public string TripAdvisorUrl { get; set; }
            public bool IsActive { get; set; }
          

    }
}
