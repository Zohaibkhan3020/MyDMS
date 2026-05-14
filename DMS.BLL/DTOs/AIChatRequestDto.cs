using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class AIChatRequestDto
    {
        public int UserID { get; set; }

        public int SessionID { get; set; }

        public string Message { get; set; }
    }
}
