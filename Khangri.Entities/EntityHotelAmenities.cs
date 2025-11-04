using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
	public class EntityHotelAmenities
	{
        public int HotelAmenitiesId { get; set; }
        public int HotelId { get; set; }
        public int AmenitiesId { get; set; }
        public bool IsActive { get; set; }

    }
}
