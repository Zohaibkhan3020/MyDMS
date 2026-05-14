using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using DMS.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DMS.BLL.Services
{
    

    public class AIMetadataService
        : IAIMetadataService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly IOCRRepository
            _ocrRepository;

        private readonly IAIMetadataRepository
            _repository;

        public AIMetadataService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            IOCRRepository ocrRepository,

            IAIMetadataRepository repository)
        {
            _configuration =
                configuration;

            _vaultRepository =
                vaultRepository;

            _ocrRepository =
                ocrRepository;

            _repository =
                repository;
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

        public async Task<AIResultDto>
            AnalyzeAsync(
                AnalyzeDocumentDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var text =
                await _ocrRepository
                    .GetOCRTextAsync(
                        connectionString,
                        dto.DocumentID);

            var result =
                AnalyzeText(text);

            foreach (var item
                in result.Metadata)
            {
                await _repository
                    .SaveMetadataAsync(
                        connectionString,

                        new AIMetadata
                        {
                            DocumentID =
                                dto.DocumentID,

                            MetadataKey =
                                item.Key,

                            MetadataValue =
                                item.Value,

                            ConfidenceScore =
                                95
                        });
            }

            foreach (var tag
                in result.Tags)
            {
                await _repository
                    .SaveTagAsync(
                        connectionString,

                        new AITag
                        {
                            DocumentID =
                                dto.DocumentID,

                            TagName =
                                tag
                        });
            }

            return new AIResultDto
            {
                DocumentID =
                    dto.DocumentID,

                DocumentType =
                    result.DocumentType,

                SuggestedFolder =
                    result.SuggestedFolder,

                Metadata =
                    result.Metadata,

                Tags =
                    result.Tags
            };
        }

        private AIAnalysisResult
            AnalyzeText(
                string text)
        {
            var result =
                new AIAnalysisResult
                {
                    Metadata =
                        new Dictionary<string, string>(),

                    Tags =
                        new List<string>()
                };

            if (text.Contains("Invoice"))
            {
                result.DocumentType =
                    "Invoice";

                result.SuggestedFolder =
                    "Finance";

                result.Tags.Add(
                    "invoice");

                result.Tags.Add(
                    "payment");

                result.Metadata.Add(
                    "Department",
                    "Finance");
            }

            if (text.Contains("Employee"))
            {
                result.DocumentType =
                    "Employee File";

                result.SuggestedFolder =
                    "HR";

                result.Tags.Add(
                    "employee");

                result.Tags.Add(
                    "hr");
            }

            return result;
        }
    }
}
