using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class AIResultDto
    {
        public int DocumentID { get; set; }

        public string DocumentType { get; set; }

        public string SuggestedFolder { get; set; }

        public Dictionary<string, string>
            Metadata
        { get; set; }

        public List<string>
            Tags
        { get; set; }
    }
}
