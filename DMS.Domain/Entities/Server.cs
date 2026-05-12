using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class Server
    {
        public int ServerID { get; set; }

        public string ServerName { get; set; }

        public string ServerIP { get; set; }

        public string DatabaseServer { get; set; }

        public string StorageRootPath { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
    }
}
