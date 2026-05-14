using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class SearchRequest
    {
        public int VaultID { get; set; }

        public string Keyword { get; set; }
    }
}
