using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class Vault
    {
        public int VaultID { get; set; }

        public int ServerID { get; set; }

        public string VaultName { get; set; }

        public string DatabaseName { get; set; }

        public string FileRootPath { get; set; }

        public bool IsActive { get; set; }
    }
}
