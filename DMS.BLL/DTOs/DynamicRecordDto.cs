using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class DynamicRecordDto
    {
        public int DocumentID { get; set; }

        public string Title { get; set; }

        public List<DynamicPropertyDto> Properties
        { get; set; }
    }
}
