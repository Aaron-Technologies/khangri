using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityRoomType
    {
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageFile { get; set; }
        public bool IsActive { get; set; }
    }
}
