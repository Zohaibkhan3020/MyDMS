using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class WorkflowHistory
    {
        public int HistoryID { get; set; }

        public int DocumentID { get; set; }

        public int FromStateID { get; set; }

        public int ToStateID { get; set; }

        public string ActionName { get; set; }

        public int ActionBy { get; set; }

        public DateTime ActionDate { get; set; }

        public string Comments { get; set; }
    }
}
