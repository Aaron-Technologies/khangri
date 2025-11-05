using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityTourImage
    {
        public int TourImageId { get; set; }
        public int TourId { get; set; }
        public int ImageId { get; set; }
        public string TourName { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
    }
}
