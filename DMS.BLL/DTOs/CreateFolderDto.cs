using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateFolderDto
    {
        public int VaultID { get; set; }

        public int? ParentFolderID { get; set; }

        public string FolderName { get; set; }
    }
}
