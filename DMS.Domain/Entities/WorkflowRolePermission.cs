using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class WorkflowRolePermission
    {
        public int WorkflowRolePermissionID { get; set; }

        public int WorkflowID { get; set; }

        public int StateID { get; set; }

        public int RoleID { get; set; }

        public bool CanApprove { get; set; }

        public bool CanReject { get; set; }
    }
}
