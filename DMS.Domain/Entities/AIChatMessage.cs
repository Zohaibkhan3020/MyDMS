using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class AIChatMessage
    {
        public int MessageID { get; set; }

        public int SessionID { get; set; }

        public string SenderType { get; set; }

        public string MessageText { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
