
using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DMS.BLL.Services
{
    public class DynamicRecordService
        : IDynamicRecordService
    {
        private readonly IVaultRepository
            _vaultRepository;

        private readonly IDynamicRecordRepository
            _repository;

        private readonly IConfiguration
            _configuration;

        public DynamicRecordService(
            IVaultRepository vaultRepository,

            IDynamicRecordRepository repository,

            IConfiguration configuration)
        {
            _vaultRepository =
                vaultRepository;

            _repository =
                repository;

            _configuration =
                configuration;
        }

        private async Task<string>
            BuildVaultConnection(
                int vaultId)
        {
            var vault =
                await _vaultRepository
                    .GetByIdAsync(vaultId);

            var masterConnection =
                _configuration
                    .GetConnectionString(
                        "MasterConnection");

            var builder =
                new SqlConnectionStringBuilder(
                    masterConnection);

            builder.InitialCatalog =
                vault.DatabaseName;

            return builder.ConnectionString;
        }

        public async Task<int>
            SaveAsync(
                SaveDynamicRecordDto dto,
                int userId)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var document =
                new Document
                {
                    ObjectTypeID =
                        dto.ObjectTypeID,

                    Title =
                        dto.Title,

                    CreatedBy =
                        userId,

                    IsDeleted = false
                };

            var documentId =
                await _repository
                    .InsertDocumentAsync(
                        connectionString,
                        document);

            foreach (var item in dto.Properties)
            {
                await _repository
                    .InsertMetadataAsync(
                        connectionString,

                        new MetadataValue
                        {
                            DocumentID =
                                documentId,

                            PropertyID =
                                item.PropertyID,

                            ValueText =
                                item.Value
                        });
            }

            return documentId;
        }

        public async Task<List<DynamicRecordDto>>
    GetRecordsAsync(
        int vaultId,
        int objectTypeId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            var documents =
                await _repository
                    .GetDocumentsAsync(
                        connectionString,
                        objectTypeId);

            var result =
                new List<DynamicRecordDto>();

            foreach (var doc in documents)
            {
                var metadata =
                    await _repository
                        .GetMetadataAsync(
                            connectionString,
                            doc.DocumentID);

                var properties =
                    metadata
                        .Select(x =>
                            new DynamicPropertyDto
                            {
                                PropertyName =
                                    x.DisplayName,

                                Value =
                                    x.ValueText
                            })
                        .ToList();

                result.Add(
                    new DynamicRecordDto
                    {
                        DocumentID =
                            doc.DocumentID,

                        Title =
                            doc.Title,

                        Properties =
                            properties
                    });
            }

            return result;
        }
    }
}
