using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class WorkflowActionDto
    {
        public int VaultID { get; set; }

        public int DocumentID { get; set; }

        public int TransitionID { get; set; }

        public string Comments { get; set; }
    }
}
