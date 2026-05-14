using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentCheckout
    {
        public int CheckoutID { get; set; }

        public int DocumentID { get; set; }

        public int CheckedOutBy { get; set; }

        public DateTime CheckedOutOn { get; set; }

        public bool IsActive { get; set; }

        public string MachineName { get; set; }

        public string IPAddress { get; set; }

        public string Comments { get; set; }
    }
}
