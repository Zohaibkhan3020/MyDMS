using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.DTOs
{
    public class SearchResponseDto
    {
        public int DocumentID { get; set; }

        public string Title { get; set; }

        public string MatchText { get; set; }
    }
}
