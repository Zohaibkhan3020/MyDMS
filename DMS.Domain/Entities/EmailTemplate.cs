using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class EmailTemplate
    {
        public int TemplateID { get; set; }

        public string TemplateName { get; set; }

        public string Subject { get; set; }

        public string BodyHTML { get; set; }

        public bool IsActive { get; set; }
    }
}
