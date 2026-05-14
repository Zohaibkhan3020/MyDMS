using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class AIMemory
    {
        public int MemoryID { get; set; }

        public int UserID { get; set; }

        public string MemoryKey { get; set; }

        public string MemoryValue { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
