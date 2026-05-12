using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    public class ObjectTypeService
    : IObjectTypeService
    {
        private readonly IVaultRepository _vaultRepository;

        private readonly IObjectTypeRepository  _repository;

        private readonly IConfiguration _configuration;

        public ObjectTypeService(IVaultRepository vaultRepository,IObjectTypeRepository repository,
            IConfiguration configuration)
        {
            _vaultRepository = vaultRepository;
            _repository = repository;
            _configuration =  configuration;
        }

        private async Task<string>BuildVaultConnection(int vaultId)
        {
            var vault = await _vaultRepository.GetByIdAsync(vaultId);

            var masterConnection = _configuration.GetConnectionString("MasterConnection");

            var builder =  new SqlConnectionStringBuilder(masterConnection);

            builder.InitialCatalog = vault.DatabaseName;

            return builder.ConnectionString;
        }

        public async Task<int> CreateAsync(CreateObjectTypeDto dto,int userId)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var entity =
                new ObjectType
                {
                    ObjectTypeName =
                        dto.ObjectTypeName,

                    TableName =
                        "OBJ_" +
                        dto.ObjectTypeName
                            .Replace(" ", "_")
                            .ToUpper(),

                    HasFiles =
                        dto.HasFiles,

                    IsSystem = false,

                    IsActive = true,

                    CreatedBy = userId
                };

            return await _repository
                .InsertAsync(
                    connectionString,
                    entity);
        }

        public async Task UpdateAsync(
            UpdateObjectTypeDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var entity =
                new ObjectType
                {
                    ObjectTypeID =
                        dto.ObjectTypeID,

                    ObjectTypeName =
                        dto.ObjectTypeName,

                    HasFiles =
                        dto.HasFiles,

                    IsActive =
                        dto.IsActive
                };

            await _repository
                .UpdateAsync(
                    connectionString,
                    entity);
        }

        public async Task DeleteAsync(
            int vaultId,
            int objectTypeId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            await _repository
                .DeleteAsync(
                    connectionString,
                    objectTypeId);
        }

        public async Task<List<ObjectType>>
            GetAllAsync(
                int vaultId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            return await _repository
                .GetAllAsync(
                    connectionString);
        }

        public async Task<ObjectType>
            GetByIdAsync(
                int vaultId,
                int objectTypeId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            return await _repository
                .GetByIdAsync(
                    connectionString,
                    objectTypeId);
        }
    }
}
