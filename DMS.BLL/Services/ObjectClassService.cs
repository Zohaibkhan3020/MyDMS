
using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DMS.BLL.Services
{

    public class ObjectClassService
        : IObjectClassService
    {
        private readonly IVaultRepository
            _vaultRepository;

        private readonly IObjectClassRepository
            _repository;

        private readonly IConfiguration
            _configuration;

        public ObjectClassService(
            IVaultRepository vaultRepository,

            IObjectClassRepository repository,

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
            CreateAsync(
                CreateObjectClassDto dto,
                int userId)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var entity =
                new ObjectClass
                {
                    ObjectTypeID =
                        dto.ObjectTypeID,

                    ClassName =
                        dto.ClassName,

                    Description =
                        dto.Description,

                    IconName =
                        dto.IconName,

                    ColorCode =
                        dto.ColorCode,

                    IsActive = true,

                    CreatedBy = userId
                };

            return await _repository
                .InsertAsync(
                    connectionString,
                    entity);
        }

        public async Task UpdateAsync(
            UpdateObjectClassDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var entity =
                new ObjectClass
                {
                    ClassID = dto.ClassID,

                    ObjectTypeID =
                        dto.ObjectTypeID,

                    ClassName =
                        dto.ClassName,

                    Description =
                        dto.Description,

                    IconName =
                        dto.IconName,

                    ColorCode =
                        dto.ColorCode,

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
            int classId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            await _repository
                .DeleteAsync(
                    connectionString,
                    classId);
        }

        public async Task<List<ObjectClass>>
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

        public async Task<List<ObjectClass>>
            GetByObjectTypeAsync(
                int vaultId,
                int objectTypeId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            return await _repository
                .GetByObjectTypeAsync(
                    connectionString,
                    objectTypeId);
        }

        public async Task<ObjectClass>
            GetByIdAsync(
                int vaultId,
                int classId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            return await _repository
                .GetByIdAsync(
                    connectionString,
                    classId);
        }
    }
}
