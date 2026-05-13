using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateStateDto
    {
        public int VaultID { get; set; }

        public int WorkflowID { get; set; }

        public string StateName { get; set; }

        public string StateColor { get; set; }

        public bool IsInitial { get; set; }

        public bool IsFinal { get; set; }

        public int SortOrder { get; set; }
    }
}
