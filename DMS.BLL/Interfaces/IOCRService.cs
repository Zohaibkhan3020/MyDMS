using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IOCRService
    {
        Task<OCRResultDto>
            ExtractAsync(
                int vaultId,
                int documentId,
                string filePath,
                int userId);

        Task<string>
            GetOCRTextAsync(
                int vaultId,
                int documentId);
    }
}
