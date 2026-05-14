using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class SearchKeyword
    {
        public int KeywordID { get; set; }

        public int DocumentID { get; set; }

        public string KeywordText { get; set; }
    }
}
