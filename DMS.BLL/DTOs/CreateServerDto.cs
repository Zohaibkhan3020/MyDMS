using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateServerDto
    {
        public string ServerName { get; set; }

        public string ServerIP { get; set; }

        public string DatabaseServer { get; set; }

        public string StorageRootPath { get; set; }
    }
}
