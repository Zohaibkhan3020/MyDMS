using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class AlertRule
    {
        public int AlertRuleID { get; set; }

        public string RuleName { get; set; }

        public string AlertType { get; set; }

        public int DaysBefore { get; set; }

        public bool IsActive { get; set; }
    }
}
