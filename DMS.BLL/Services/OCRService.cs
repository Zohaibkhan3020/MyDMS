using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tesseract;

using UglyToad.PdfPig;

namespace DMS.BLL.Services
{
    

    

    public class OCRService
        : IOCRService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly IOCRRepository
            _repository;

        public OCRService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            IOCRRepository repository)
        {
            _configuration =
                configuration;

            _vaultRepository =
                vaultRepository;

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

        public async Task<OCRResultDto>
            ExtractAsync(
                int vaultId,
                int documentId,
                string filePath,
                int userId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            string extractedText = "";

            var extension =
                Path.GetExtension(filePath)
                    .ToLower();

            if (extension == ".pdf")
            {
                using var pdf =
                    PdfDocument.Open(filePath);

                foreach (var page in pdf.GetPages())
                {
                    extractedText +=
                        page.Text + "\n";
                }
            }
            else
            {
                using var engine =
                    new TesseractEngine(
                        "./OCR/tessdata",
                        "eng",
                        EngineMode.Default);

                using var image =
                    Pix.LoadFromFile(filePath);

                using var page =
                    engine.Process(image);

                extractedText =
                    page.GetText();
            }

            await _repository
                .SaveOCRTextAsync(
                    connectionString,

                    new OCRContent
                    {
                        DocumentID =
                            documentId,

                        ExtractedText =
                            extractedText,

                        ExtractedBy =
                            userId
                    });

            var metadata =
                ExtractMetadata(
                    extractedText);

            foreach (var item in metadata)
            {
                await _repository
                    .SaveMetadataAsync(
                        connectionString,

                        new OCRMetadata
                        {
                            DocumentID =
                                documentId,

                            MetadataKey =
                                item.Key,

                            MetadataValue =
                                item.Value
                        });
            }

            return new OCRResultDto
            {
                DocumentID =
                    documentId,

                ExtractedText =
                    extractedText,

                Metadata =
                    metadata
            };
        }

        public async Task<string>
            GetOCRTextAsync(
                int vaultId,
                int documentId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            return await _repository
                .GetOCRTextAsync(
                    connectionString,
                    documentId);
        }

        private Dictionary<string, string>
            ExtractMetadata(
                string text)
        {
            var result =
                new Dictionary<string, string>();

            if (text.Contains("Invoice"))
            {
                result.Add(
                    "DocumentType",
                    "Invoice");
            }

            if (text.Contains("CNIC"))
            {
                result.Add(
                    "DocumentType",
                    "CNIC");
            }

            return result;
        }
    }
}
