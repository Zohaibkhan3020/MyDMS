using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentLock
    {
        public int LockID { get; set; }

        public int DocumentID { get; set; }

        public int LockedBy { get; set; }

        public DateTime LockedOn { get; set; }

        public string LockType { get; set; }

        public bool IsLocked { get; set; }
    }
}
