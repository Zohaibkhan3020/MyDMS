using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentWatermark
    {
        public int WatermarkID { get; set; }

        public int DocumentID { get; set; }

        public string WatermarkText { get; set; }

        public DateTime AppliedOn { get; set; }
    }
}
