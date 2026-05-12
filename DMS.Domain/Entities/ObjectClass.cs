using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class ObjectClass
    {
        public int ClassID { get; set; }

        public int ObjectTypeID { get; set; }

        public string ClassName { get; set; }

        public string Description { get; set; }

        public string IconName { get; set; }

        public string ColorCode { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }
    }
}
