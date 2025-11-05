using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EntityResetPasswordInitiativeResponse
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Token { get; set; }
    }
}
