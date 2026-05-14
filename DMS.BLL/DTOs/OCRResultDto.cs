using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class OCRResultDto
    {
        public int DocumentID { get; set; }

        public string ExtractedText { get; set; }

        public Dictionary<string, string>
            Metadata
        { get; set; }
    }
}
