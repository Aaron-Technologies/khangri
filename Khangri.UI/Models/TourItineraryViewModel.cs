using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class TourItineraryViewModel
    {
        public int ItineraryId { get; set; }
        public int TourId { get; set; }

        [Required(ErrorMessage ="Please Enter TourName")]
        public string TourName { get; set; }

        [Required(ErrorMessage ="Please Enter Number Of Day")]
        public int Day { get; set; }

        [Required(ErrorMessage ="Please Enter Itinery")]
        public string Itinery { get; set; }

        [Required(ErrorMessage ="Please Enter ItineryDetails")]
        public string ItineryDetails { get; set; }

        [Required(ErrorMessage ="Please Select Image")]
        public int ImageId { get; set; }
        public bool IsActive { get; set; }

    }
}
