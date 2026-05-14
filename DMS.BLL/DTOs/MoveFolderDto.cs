using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class MoveFolderDto
    {
        public int VaultID { get; set; }

        public int FolderID { get; set; }

        public int? NewParentFolderID { get; set; }
    }
}
