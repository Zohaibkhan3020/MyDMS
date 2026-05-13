using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IFileService
    {
        Task<int> UploadAsync(
            UploadFileDto dto,
            int userId);
        Task<FileEntity>
        GetFileAsync(
            int vaultId,
            int fileId);

        Task<List<FilePreview>>
        GetByDocumentAsync(
            int vaultId,
            int documentId);
    }
}
