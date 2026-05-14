using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class WorkflowPermissionDto
    {
        public int VaultID { get; set; }

        public int WorkflowID { get; set; }

        public int StateID { get; set; }

        public int RoleID { get; set; }

        public bool CanApprove { get; set; }

        public bool CanReject { get; set; }
    }
}
