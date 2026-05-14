using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class SearchResponse
    {
        public int DocumentID { get; set; }

        public string Title { get; set; }

        public string MatchText { get; set; }
    }
}
