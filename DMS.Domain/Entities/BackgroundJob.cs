using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class BackgroundJob
    {
        public int JobID { get; set; }

        public string JobType { get; set; }

        public string Payload { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public string ErrorMessage { get; set; }
    }
}
