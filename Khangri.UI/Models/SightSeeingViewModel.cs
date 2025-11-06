using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class SightSeeingViewModel
    {
        public int SightSeeingId { get; set; }

        [Required(ErrorMessage ="Please Enter SightSeeing Name")]
        public string SightSeeing { get; set; }

        //[Required(ErrorMessage = "Please Enter an EstimateTime")]
        public string EstimateTime { get; set; }

        public string ImageFile { get; set; }

        [Required(ErrorMessage ="Please Enter Details")]
        public string Details { get; set; }

        //[Required(ErrorMessage = "Please Enter Altitude")]
        // public string Altitude { get; set; }

        //[Required(ErrorMessage = "Please Enter Google Map Url")]
        // public string GoogleMapUrl { get; set; } 
        public bool IsActive { get; set; }
    }
}
