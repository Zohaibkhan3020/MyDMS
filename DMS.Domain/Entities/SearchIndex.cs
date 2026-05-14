using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class SearchIndex
    {
        public int SearchIndexID { get; set; }

        public int DocumentID { get; set; }

        public string IndexedText { get; set; }

        public DateTime IndexedOn { get; set; }
    }
}
