using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DMS.BLL.DTOs
{

    public class UploadVersionDto
    {
        public int VaultID { get; set; }

        public int DocumentID { get; set; }

        public int FileID { get; set; }

        public string Comments { get; set; }

        public IFormFile File { get; set; }
    }
}
