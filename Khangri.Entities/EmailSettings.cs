using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khangri.Entities
{
    public class EmailSettings
    {
        public string SmtpServer { get; init; }
        public int Port { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string FromAddress { get; init; }
        public bool UseSsl { get; init; }
        public string FromUsername { get; init; }
    }
}
