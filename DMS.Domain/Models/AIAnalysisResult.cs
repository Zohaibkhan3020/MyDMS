using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Models
{
    public class AIAnalysisResult
    {
        public Dictionary<string, string>
            Metadata
        { get; set; }

        public List<string>
            Tags
        { get; set; }

        public string SuggestedFolder { get; set; }

        public string DocumentType { get; set; }
    }
}
