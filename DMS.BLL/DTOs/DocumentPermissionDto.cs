using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class DocumentPermissionDto
    {
        public int VaultID { get; set; }

        public int DocumentID { get; set; }

        public int? UserID { get; set; }

        public int? RoleID { get; set; }

        public bool CanView { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool CanDownload { get; set; }
    }
}
