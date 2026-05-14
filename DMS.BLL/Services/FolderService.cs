using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DMS.BLL.Services
{
    

    public class FolderService
        : IFolderService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly IFolderRepository
            _repository;

        public FolderService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            IFolderRepository repository)
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

        public async Task<int>
            CreateFolderAsync(
                CreateFolderDto dto,
                int userId)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            string path =
                dto.FolderName;

            int level = 0;

            if (dto.ParentFolderID.HasValue)
            {
                var parent =
                    await _repository
                        .GetFolderAsync(
                            connectionString,
                            dto.ParentFolderID.Value);

                path =
                    parent.FolderPath +
                    "/" +
                    dto.FolderName;

                level =
                    parent.FolderLevel + 1;
            }

            return await _repository
                .CreateFolderAsync(
                    connectionString,

                    new Folder
                    {
                        ParentFolderID =
                            dto.ParentFolderID,

                        FolderName =
                            dto.FolderName,

                        FolderPath =
                            path,

                        FolderLevel =
                            level,

                        CreatedBy =
                            userId
                    });
        }

        public async Task<List<FolderTreeDto>>
            GetTreeAsync(
                int vaultId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            var folders =
                await _repository
                    .GetAllFoldersAsync(
                        connectionString);

            List<FolderTreeDto>
                BuildTree(
                    int? parentId)
            {
                return folders
                    .Where(x =>
                        x.ParentFolderID ==
                        parentId)

                    .Select(x =>
                        new FolderTreeDto
                        {
                            FolderID =
                                x.FolderID,

                            FolderName =
                                x.FolderName,

                            FolderPath =
                                x.FolderPath,

                            Children =
                                BuildTree(
                                    x.FolderID)
                        })
                    .ToList();
            }

            return BuildTree(null);
        }

        public async Task MoveFolderAsync(
            MoveFolderDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .MoveFolderAsync(
                    connectionString,
                    dto.FolderID,
                    dto.NewParentFolderID);
        }
    }
}
