using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentWorkflow
    {
        public int DocumentWorkflowID { get; set; }

        public int DocumentID { get; set; }

        public int WorkflowID { get; set; }

        public int CurrentStateID { get; set; }

        public int StartedBy { get; set; }

        public DateTime StartedOn { get; set; }

        public bool Completed { get; set; }
    }
}
