using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateWorkflowDto
    {
        public int VaultID { get; set; }

        public string WorkflowName { get; set; }

        public string Description { get; set; }
    }
}
