using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class PropertyPermission
    {
        public int PropertyPermissionID { get; set; }

        public int PropertyID { get; set; }

        public int RoleID { get; set; }

        public bool CanView { get; set; }

        public bool CanEdit { get; set; }
    }
}
