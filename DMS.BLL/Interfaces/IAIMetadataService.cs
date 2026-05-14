using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IAIMetadataService
    {
        Task<AIResultDto>
            AnalyzeAsync(
                AnalyzeDocumentDto dto);
    }
}
