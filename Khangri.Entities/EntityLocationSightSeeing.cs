using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityLocationSightSeeing
    {
        public int LocationSightSeeingId { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public long SightSeeingId { get; set; }
        public string SightSeen { get; set; }
        public int DistanceFromLocation { get; set; }
        public bool IsActive { get; set; }
    }
}
