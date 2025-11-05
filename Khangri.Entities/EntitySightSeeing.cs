using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntitySightSeeing
    {
        public int SightSeeingId { get; set; }
        public string SightSeeing { get; set; }
        public string EstimateTime { get; set; }
        public string ImageFile { get; set; }
        public string Details { get; set; }
        public string Altitude { get; set; }
        public string GoogleMapUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
