using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class SearchResult
    {
        public int DocumentID { get; set; }

        public string Title { get; set; }

        public string ObjectType { get; set; }

        public string PropertyName { get; set; }

        public string Value { get; set; }
    }
}
