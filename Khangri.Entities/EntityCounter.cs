using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityCounter
    {
        public int CounterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Value { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
    }
}
