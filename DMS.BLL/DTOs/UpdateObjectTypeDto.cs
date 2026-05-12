using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class UpdateObjectTypeDto
    {
        public int ObjectTypeID { get; set; }

        public int VaultID { get; set; }

        public string ObjectTypeName { get; set; }

        public bool HasFiles { get; set; }

        public bool IsActive { get; set; }
    }
}
