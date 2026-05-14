using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class OCRContent
    {
        public int OCRID { get; set; }

        public int DocumentID { get; set; }

        public string ExtractedText { get; set; }

        public DateTime ExtractedOn { get; set; }

        public int ExtractedBy { get; set; }
    }
}
