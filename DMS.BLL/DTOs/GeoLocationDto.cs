using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class GeoLocationDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ISP { get; set; }
    }
}
