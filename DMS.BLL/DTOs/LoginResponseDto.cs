using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class LoginResponseDto
    {
        public int UserID { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public DateTime Expiry { get; set; }
    }
}
