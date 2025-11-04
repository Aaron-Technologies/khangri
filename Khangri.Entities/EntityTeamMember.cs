using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityTeamMember
    {
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public int ImageId { get; set; }
        public string Caption { get; set; }
        public string Designation { get; set; }
        public bool IsActive { get; set; }
    }
}
