using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DMS.BLL.Services
{
    
    public class FileService
        : IFileService
    {
        private readonly IVaultRepository
            _vaultRepository;

        private readonly IFileRepository
            _repository;

        private readonly IConfiguration
            _configuration;

        public FileService(
            IVaultRepository vaultRepository,

            IFileRepository repository,

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
            UploadAsync(
                UploadFileDto dto,
                int userId)
        {
            var vault =
                await _vaultRepository
                    .GetByIdAsync(
                        dto.VaultID);

            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var extension =
                Path.GetExtension(
                    dto.File.FileName);

            var uniqueFileName =
                Guid.NewGuid() + extension;

            var folder =
                Path.Combine(
                    vault.FileRootPath,
                    DateTime.Now.Year.ToString(),
                    DateTime.Now.Month.ToString("00"));

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fullPath =
                Path.Combine(
                    folder,
                    uniqueFileName);

            using (var stream =
                   new FileStream(
                       fullPath,
                       FileMode.Create))
            {
                await dto.File
                    .CopyToAsync(stream);
            }

            var file =
                new FileEntity
                {
                    FileName =
                        uniqueFileName,

                    OriginalFileName =
                        dto.File.FileName,

                    FileExtension =
                        extension,

                    FilePath =
                        fullPath,

                    FileSize =
                        dto.File.Length,

                    ContentType =
                        dto.File.ContentType,

                    UploadedBy =
                        userId,

                    IsDeleted = false
                };

            var fileId =
                await _repository
                    .InsertFileAsync(
                        connectionString,
                        file);

            await _repository
                .LinkDocumentFileAsync(
                    connectionString,

                    new DocumentFile
                    {
                        DocumentID =
                            dto.DocumentID,

                        FileID =
                            fileId
                    });

            return fileId;
        }
    }
}
