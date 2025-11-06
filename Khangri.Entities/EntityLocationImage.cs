using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityLocationImage
    {
        public int LocationImageId { get; set; }
        public int ImageId { get; set; }
        public long LocationId { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }

    }
}
