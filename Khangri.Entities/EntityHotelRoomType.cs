using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
	public class EntityHotelRoomType
	{
        public int HotelRoomTypeId { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Price { get; set; }
        public string ImageFile { get; set; }
        public bool IsActive { get; set; }
    }
}
