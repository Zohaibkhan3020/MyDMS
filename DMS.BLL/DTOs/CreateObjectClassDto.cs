using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateObjectClassDto
    {
        public int VaultID { get; set; }

        public int ObjectTypeID { get; set; }

        public string ClassName { get; set; }

        public string Description { get; set; }

        public string IconName { get; set; }

        public string ColorCode { get; set; }
    }
}
