using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentFile
    {
        public int DocumentFileID { get; set; }

        public int DocumentID { get; set; }

        public int FileID { get; set; }
    }
}
