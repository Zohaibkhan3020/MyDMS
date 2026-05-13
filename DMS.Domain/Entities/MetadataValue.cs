using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class MetadataValue
    {
        public int MetadataID { get; set; }

        public int DocumentID { get; set; }

        public int PropertyID { get; set; }

        public string ValueText { get; set; }
    }
}
