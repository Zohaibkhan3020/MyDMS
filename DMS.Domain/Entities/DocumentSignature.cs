using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentSignature
    {
        public int SignatureID { get; set; }

        public int DocumentID { get; set; }

        public int SignedBy { get; set; }

        public string SignatureHash { get; set; }

        public DateTime SignedOn { get; set; }

        public bool IsValid { get; set; }
    }
}
