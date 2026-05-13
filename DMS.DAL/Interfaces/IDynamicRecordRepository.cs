using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IDynamicRecordRepository
    {
        Task<int> InsertDocumentAsync(
            string connectionString,
            Document document);

        Task InsertMetadataAsync(
            string connectionString,
            MetadataValue metadata);

        Task<List<Document>>
    GetDocumentsAsync(
        string connectionString,
        int objectTypeId);

        Task<List<dynamic>>
            GetMetadataAsync(
                string connectionString,
                int documentId);
    }
}
