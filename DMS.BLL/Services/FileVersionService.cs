using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DMS.BLL.Services
{
    
    public class FileVersionService
        : IFileVersionService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly IFileVersionRepository
            _repository;

        public FileVersionService(IConfiguration configuration,IVaultRepository vaultRepository,
            IFileVersionRepository repository)
        {
            _configuration = configuration;
            _vaultRepository = vaultRepository;
            _repository = repository;
        }

        private async Task<string> BuildVaultConnection(int vaultId)
        {
            var vault = await _vaultRepository.GetByIdAsync(vaultId);
            var masterConnection =_configuration.GetConnectionString("MasterConnection");

            var builder = new SqlConnectionStringBuilder(masterConnection);
            builder.InitialCatalog =  vault.DatabaseName;
            return builder.ConnectionString;
        }

        public async Task UploadVersionAsync(UploadVersionDto dto,int userId)
        {
            var vault = await _vaultRepository.GetByIdAsync(dto.VaultID);

            var connectionString = await BuildVaultConnection(dto.VaultID);

            var versionNo = await _repository.GetNextVersionAsync(connectionString,dto.FileID);

            var extension = Path.GetExtension(dto.File.FileName);

            var uniqueName =  Guid.NewGuid() + extension;

            var folder = Path.Combine(vault.FileRootPath, "Versions", dto.FileID.ToString());

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fullPath = Path.Combine(folder,uniqueName);

            using (var stream = new FileStream(fullPath,FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            await _repository.ResetCurrentVersionAsync(connectionString,dto.FileID);

            await _repository.InsertAsync(connectionString, new FileVersion
            {
                DocumentID =  dto.DocumentID,
                FileID = dto.FileID,
                VersionNo = versionNo,
                VersionLabel = "v" + versionNo,
                FilePath = fullPath,
                FileSize = dto.File.Length,
                ContentType = dto.File.ContentType,
                UploadedBy = userId,
                Comments = dto.Comments,
                IsCurrent = true
            });
        }

        public async Task<List<FileVersionDto>>GetVersionsAsync(int vaultId,int fileId)
        {
            var connectionString =  await BuildVaultConnection(vaultId);

            var data = await _repository.GetVersionsAsync(connectionString,fileId);

            return data.Select(x => new FileVersionDto
                {
                    VersionID = x.VersionID,

                    VersionNo = x.VersionNo,

                    VersionLabel = x.VersionLabel,

                    ContentType = x.ContentType,

                    FileSize = x.FileSize,

                    UploadedOn = x.UploadedOn,

                    Comments = x.Comments,

                    IsCurrent = x.IsCurrent
                }).ToList();
        }
    }
}
