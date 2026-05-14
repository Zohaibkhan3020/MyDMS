using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class FolderTreeDto
    {
        public int FolderID { get; set; }

        public string FolderName { get; set; }

        public string FolderPath { get; set; }

        public List<FolderTreeDto> Children { get; set; }
    }
}
