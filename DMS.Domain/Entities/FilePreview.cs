using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class FilePreview
    {
        public int FileID { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string ContentType { get; set; }

        public long FileSize { get; set; }
    }
}
