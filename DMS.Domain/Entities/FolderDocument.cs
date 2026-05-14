using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class FolderDocument
    {
        public int FolderDocumentID { get; set; }

        public int FolderID { get; set; }

        public int DocumentID { get; set; }
    }
}
