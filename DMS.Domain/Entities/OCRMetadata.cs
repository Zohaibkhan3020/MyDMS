using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class OCRMetadata
    {
        public int OCRMetadataID { get; set; }

        public int DocumentID { get; set; }

        public string MetadataKey { get; set; }

        public string MetadataValue { get; set; }
    }
}
