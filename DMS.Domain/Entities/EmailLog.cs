using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class EmailLog
    {
        public int EmailLogID { get; set; }

        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string BodyHTML { get; set; }

        public DateTime? SentOn { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
