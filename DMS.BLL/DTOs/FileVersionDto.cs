using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class FileVersionDto
    {
        public int VersionID { get; set; }

        public int VersionNo { get; set; }

        public string VersionLabel { get; set; }

        public string ContentType { get; set; }

        public long FileSize { get; set; }

        public DateTime UploadedOn { get; set; }

        public string Comments { get; set; }

        public bool IsCurrent { get; set; }
    }
}
