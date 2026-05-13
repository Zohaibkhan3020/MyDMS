using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class WorkflowTransition
    {
        public int TransitionID { get; set; }

        public int WorkflowID { get; set; }

        public int FromStateID { get; set; }

        public int ToStateID { get; set; }

        public string ActionName { get; set; }

        public int AllowedRoleID { get; set; }
    }
}
