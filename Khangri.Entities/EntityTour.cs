using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityTour
    {
        public int TourId { get; set; }
        public int TourTypeId { get; set; }
        public string TourName { get; set; }
        public string TourInroduction { get; set; }
        public string TourHighlight { get; set; }
        public string TourOverview { get; set; }
        public int NoOfDays { get; set; }
        public string Price { get; set; }
        public bool IsActive { get; set; }
		public int M20_ImageId { get; set; }
		public string ImageName { get; set; }
		public string Caption { get; set; }
	}
}
