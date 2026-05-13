using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class UpdateObjectPropertyDto
    {
        public int PropertyID { get; set; }

        public int VaultID { get; set; }

        public int ObjectTypeID { get; set; }

        public string PropertyName { get; set; }

        public string DisplayName { get; set; }

        public string DataType { get; set; }

        public string ControlType { get; set; }

        public bool IsRequired { get; set; }

        public bool IsUnique { get; set; }

        public bool IsSearchable { get; set; }

        public string DefaultValue { get; set; }

        public int? LookupObjectTypeID { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
