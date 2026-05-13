using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class FileVersion
    {
        public int VersionID { get; set; }

        public int DocumentID { get; set; }

        public int FileID { get; set; }

        public int VersionNo { get; set; }

        public string VersionLabel { get; set; }

        public string FilePath { get; set; }

        public long FileSize { get; set; }

        public string ContentType { get; set; }

        public int UploadedBy { get; set; }

        public DateTime UploadedOn { get; set; }

        public string Comments { get; set; }

        public bool IsCurrent { get; set; }
    }
}
