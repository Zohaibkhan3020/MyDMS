using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class ObjectType
    {
        public int ObjectTypeID { get; set; }

        public string ObjectTypeName { get; set; }

        public string TableName { get; set; }

        public bool HasFiles { get; set; }

        public bool IsSystem { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }
    }
}
