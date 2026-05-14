using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IOCRRepository
    {
        Task SaveOCRTextAsync(
            string connectionString,
            OCRContent model);

        Task SaveMetadataAsync(
            string connectionString,
            OCRMetadata model);

        Task<string> GetOCRTextAsync(
            string connectionString,
            int documentId);
    }
}
