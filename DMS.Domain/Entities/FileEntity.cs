using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class FileEntity
    {
        public int FileID { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string FilePath { get; set; }

        public long FileSize { get; set; }

        public string ContentType { get; set; }

        public int UploadedBy { get; set; }

        public DateTime UploadedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
