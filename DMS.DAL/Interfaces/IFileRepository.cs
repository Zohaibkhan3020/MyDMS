using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IFileRepository
    {
        Task<int> InsertFileAsync(
            string connectionString,
            FileEntity file);

        Task LinkDocumentFileAsync(
            string connectionString,
            DocumentFile model);

        Task<FileEntity>
    GetByIdAsync(
        string connectionString,
        int fileId);

        Task<List<FilePreview>>
            GetByDocumentAsync(
                string connectionString,
                int documentId);
    }
}
