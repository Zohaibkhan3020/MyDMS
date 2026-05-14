using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class Folder
    {
        public int FolderID { get; set; }

        public int? ParentFolderID { get; set; }

        public string FolderName { get; set; }

        public string FolderPath { get; set; }

        public int FolderLevel { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
