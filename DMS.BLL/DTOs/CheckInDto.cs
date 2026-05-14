using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CheckInDto
    {
        public int VaultID { get; set; }

        public int DocumentID { get; set; }

        public string Comments { get; set; }
    }
}
