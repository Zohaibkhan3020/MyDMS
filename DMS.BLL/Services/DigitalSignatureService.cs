using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using PdfDocumentLayout = iText.Layout.Document;
namespace DMS.BLL.Services
{
    

    public class DigitalSignatureService
        : IDigitalSignatureService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly
            IDigitalSignatureRepository
                _repository;

        public DigitalSignatureService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            IDigitalSignatureRepository repository)
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

        public async Task<string>
            SignDocumentAsync(
                SignDocumentDto dto)
        {
            var signatureText =
                $"{dto.DocumentID}-{dto.UserID}-{DateTime.Now}";

            using var sha =
                SHA256.Create();

            var bytes =
                Encoding.UTF8
                    .GetBytes(signatureText);

            var hash =
                Convert.ToBase64String(
                    sha.ComputeHash(bytes));

            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .SaveSignatureAsync(
                    connectionString,

                    new DocumentSignature
                    {
                        DocumentID =
                            dto.DocumentID,

                        SignedBy =
                            dto.UserID,

                        SignatureHash =
                            hash,

                        IsValid =
                            true
                    });

            return hash;
        }

        public async Task<string>
            ApplyWatermarkAsync(
                WatermarkDto dto)
        {
            var vault =
                await _vaultRepository
                    .GetByIdAsync(
                        dto.VaultID);

            var filePath =
                Path.Combine(
                    vault.FileRootPath,
                    $"{dto.DocumentID}.pdf");

            var output =
                Path.Combine(
                    vault.FileRootPath,
                    $"{dto.DocumentID}_wm.pdf");

            using var pdfReader =
                new PdfReader(filePath);

            using var pdfWriter =
                new PdfWriter(output);

            using var pdfDoc =
                new PdfDocument(
                    pdfReader,
                    pdfWriter);

            var document =
    new PdfDocumentLayout(pdfDoc);

            document.Add(
                new Paragraph(
                    dto.WatermarkText));

            document.Close();

            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .SaveWatermarkAsync(
                    connectionString,

                    new DocumentWatermark
                    {
                        DocumentID =
                            dto.DocumentID,

                        WatermarkText =
                            dto.WatermarkText
                    });

            return output;
        }
    }
}
