using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateVaultDto
    {
        public int ServerID { get; set; }

        public string VaultName { get; set; }

        public string DatabaseName { get; set; }
    }
}
