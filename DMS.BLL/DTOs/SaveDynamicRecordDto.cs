using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class SaveDynamicRecordDto
    {
        public int VaultID { get; set; }

        public int ObjectTypeID { get; set; }

        public string Title { get; set; }

        public List<PropertyValueDto>
            Properties
        { get; set; }
    }
}
