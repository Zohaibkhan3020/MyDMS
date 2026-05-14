using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class CreateNotificationDto
    {
        public int UserID { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string NotificationType { get; set; }

        public int? ReferenceID { get; set; }
    }
}
