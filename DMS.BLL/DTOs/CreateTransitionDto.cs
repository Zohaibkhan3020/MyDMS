using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateTransitionDto
    {
        public int VaultID { get; set; }

        public int WorkflowID { get; set; }

        public int FromStateID { get; set; }

        public int ToStateID { get; set; }

        public string ActionName { get; set; }

        public int AllowedRoleID { get; set; }
    }
}
