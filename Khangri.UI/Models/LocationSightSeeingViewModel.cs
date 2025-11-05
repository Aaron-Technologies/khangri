using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class LocationSightSeeingViewModel
    {
        public int LocationSightSeeingId { get; set; }

        [Required(ErrorMessage ="Please Select a Location")]
        public int LocationId { get; set; }
        public string Location { get; set; }

        [Required(ErrorMessage ="Please Select a SeightSeeing")]
        public long SightSeeingId { get; set; }

        public string SightSeen { get; set; }

        [Required(ErrorMessage ="Please Select a Destination From where to Start")]
        public int DistanceFromLocation { get; set; }
        public  string Altitude { get; set; }
        public string Details { get; set; }
        public bool IsActive { get; set; }
    }
}
