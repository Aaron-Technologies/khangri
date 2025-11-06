using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityLoginUser
    {
        public long UserId { get; set; }
        public int U00_UserTypeId { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ProfilePic { get; set; }
        public DateTime EntryDateTime { get; set; }
    }
}
