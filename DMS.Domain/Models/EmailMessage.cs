using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Models
{
    public class EmailMessage
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string BodyHTML { get; set; }

        public List<string> Attachments { get; set; }
    }
}
