using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class EmailQueue
    {
        public int EmailQueueID { get; set; }

        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsSent { get; set; }

        public DateTime? SentOn { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
