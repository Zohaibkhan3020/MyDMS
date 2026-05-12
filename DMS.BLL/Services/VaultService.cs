using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    public class VaultService : IVaultService
    {
        private readonly IVaultRepository _repository;

        private readonly DatabaseCreatorService _databaseCreator;

        private readonly VaultSchemaService _schemaService;

        private readonly VaultFolderService _folderService;

        private readonly VaultPermissionService _permissionService;

        private readonly IServerRepository _serverRepository;

        public VaultService(
            IVaultRepository repository,
            DatabaseCreatorService databaseCreator,
            VaultSchemaService schemaService,
            VaultFolderService folderService,
            VaultPermissionService permissionService,
            IServerRepository serverRepository)
        {
            _repository = repository;
            _databaseCreator = databaseCreator;
            _schemaService = schemaService;
            _folderService = folderService;
            _permissionService = permissionService;
            _serverRepository = serverRepository;
        }

        public async Task<int> CreateAsync(CreateVaultDto dto, int userId,int roleId)
        {
            var hasPermission = await _permissionService.CanCreateVault(dto.ServerID, roleId);

            if (!hasPermission)
                throw new Exception("Permission denied");

            var server = await _serverRepository.GetByIdAsync(dto.ServerID);

            if (server == null)
                throw new Exception("Server not found");

            await _databaseCreator.CreateDatabaseAsync(dto.DatabaseName);

            await _schemaService.RunSchemaAsync(dto.DatabaseName);

            var folderPath =  _folderService.CreateVaultFolders(server.StorageRootPath,dto.VaultName);

            var vault = new Vault
            {
                ServerID = dto.ServerID,
                VaultName = dto.VaultName,
                DatabaseName = dto.DatabaseName,
                FileRootPath = folderPath,
                IsActive = true
            };

            return await _repository.InsertAsync(vault);
        }
    }
}
