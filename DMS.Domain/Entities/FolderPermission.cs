using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class FolderPermission
    {
        public int FolderPermissionID { get; set; }

        public int FolderID { get; set; }

        public int RoleID { get; set; }

        public bool CanView { get; set; }

        public bool CanCreate { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }
    }
}
