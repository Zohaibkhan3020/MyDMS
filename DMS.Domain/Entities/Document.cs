using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class Document
    {
        public int DocumentID { get; set; }

        public int ObjectTypeID { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
