using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DMS.BLL.Services
{
   
    public class ObjectPropertyService : IObjectPropertyService
    {
        private readonly IVaultRepository  _vaultRepository;

        private readonly IObjectPropertyRepository _repository;

        private readonly IConfiguration _configuration;

        public ObjectPropertyService(IVaultRepository vaultRepository, IObjectPropertyRepository repository,IConfiguration configuration)
        {
            _vaultRepository =
                vaultRepository;

            _repository =
                repository;

            _configuration =
                configuration;
        }

        private async Task<string>BuildVaultConnection(int vaultId)
        {
            var vault = await _vaultRepository.GetByIdAsync(vaultId);

            var masterConnection = _configuration.GetConnectionString("MasterConnection");

            var builder = new SqlConnectionStringBuilder(masterConnection);

            builder.InitialCatalog =  vault.DatabaseName;

            return builder.ConnectionString;
        }

        public async Task<int> CreateAsync(CreateObjectPropertyDto dto)
        {
            var connectionString = await BuildVaultConnection(dto.VaultID);

            var entity = new ObjectProperty
                {
                    ObjectTypeID =   dto.ObjectTypeID,

                    PropertyName =  dto.PropertyName,

                    DisplayName =  dto.DisplayName,

                    DataType = dto.DataType,

                    ControlType = dto.ControlType,

                    IsRequired = dto.IsRequired,

                    IsUnique = dto.IsUnique,

                    IsSearchable = dto.IsSearchable,

                    DefaultValue = dto.DefaultValue,

                    LookupObjectTypeID = dto.LookupObjectTypeID,

                    SortOrder = dto.SortOrder,

                    IsActive = true
                };

            return await _repository.InsertAsync(connectionString,entity);
        }

        public async Task UpdateAsync(
            UpdateObjectPropertyDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var entity =
                new ObjectProperty
                {
                    PropertyID =
                        dto.PropertyID,

                    ObjectTypeID =
                        dto.ObjectTypeID,

                    PropertyName =
                        dto.PropertyName,

                    DisplayName =
                        dto.DisplayName,

                    DataType =
                        dto.DataType,

                    ControlType =
                        dto.ControlType,

                    IsRequired =
                        dto.IsRequired,

                    IsUnique =
                        dto.IsUnique,

                    IsSearchable =
                        dto.IsSearchable,

                    DefaultValue =
                        dto.DefaultValue,

                    LookupObjectTypeID =
                        dto.LookupObjectTypeID,

                    SortOrder =
                        dto.SortOrder,

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
            int propertyId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            await _repository
                .DeleteAsync(
                    connectionString,
                    propertyId);
        }

        public async Task<List<ObjectProperty>>GetAllAsync(int vaultId)
        {
            var connectionString = await BuildVaultConnection(vaultId);

            return await _repository.GetAllAsync(connectionString);
        }

        public async Task<List<ObjectProperty>> GetByObjectTypeAsync(int vaultId,int objectTypeId, int ClassID)
        {
            var connectionString =  await BuildVaultConnection(vaultId);

            return await _repository.GetByObjectTypeAsync(connectionString,objectTypeId, ClassID);
        }

        public async Task<ObjectProperty>
            GetByIdAsync(
                int vaultId,
                int propertyId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            return await _repository
                .GetByIdAsync(
                    connectionString,
                    propertyId);
        }
    }
}
