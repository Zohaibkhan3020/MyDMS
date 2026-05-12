using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class UserSession
    {
        public int SessionID { get; set; }

        public int UserID { get; set; }

        public string JwtToken { get; set; }

        public string IPAddress { get; set; }
        public string? City { get; set; }

        public string BrowserInfo { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime ExpiryTime { get; set; }

        public bool IsActive { get; set; }
    }
}
