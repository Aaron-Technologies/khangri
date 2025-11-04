using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityLocation
    {
        public int LocationId { get; set; }
        public string Location { get; set; }
        public string Caption { get; set; }
        public int M01_LocationId { get; set; }
        public string Description { get; set; }
        public string Altitude { get; set; }
        public string ImageFiles { get; set; }
        public string WeatherUrl { get; set; }
        public string GoogleMapUrl { get; set; }
        public string TripAdvisorUrl { get; set; }
        public bool IsActive { get; set; }

    }

}
