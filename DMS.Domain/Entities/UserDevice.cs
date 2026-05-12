using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class UserDevice
    {
        public int DeviceID { get; set; }

        public int UserID { get; set; }

        public string MachineName { get; set; }

        public string LocalIP { get; set; }
    }
}
