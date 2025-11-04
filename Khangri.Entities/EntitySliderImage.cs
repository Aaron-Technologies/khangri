using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public  class EntitySliderImage
    {
        public int SliderImageId { get; set; }
        public int PageTypeId { get; set; }
        public string ImageName { get; set; }
        public string Caption { get; set; }
        public string Details1 { get; set; }
        public string Details2 { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
        public bool IsActive { get; set; }
    }
}
