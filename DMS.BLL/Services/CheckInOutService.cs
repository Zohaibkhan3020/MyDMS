using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DMS.BLL.Services
{
    

    public class CheckInOutService
        : ICheckInOutService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly ICheckInOutRepository
            _repository;

        public CheckInOutService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            ICheckInOutRepository repository)
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

        public async Task CheckOutAsync(
            CheckOutDto dto,
            int userId,
            string machineName,
            string ip)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var alreadyCheckedOut =
                await _repository
                    .IsCheckedOutAsync(
                        connectionString,
                        dto.DocumentID);

            if (alreadyCheckedOut)
            {
                throw new Exception(
                    "Document already checked out");
            }

            await _repository
                .CheckOutAsync(
                    connectionString,

                    new DocumentCheckout
                    {
                        DocumentID =
                            dto.DocumentID,

                        CheckedOutBy =
                            userId,

                        MachineName =
                            machineName,

                        IPAddress =
                            ip,

                        Comments =
                            dto.Comments,

                        IsActive = true
                    });

            await _repository
                .CreateLockAsync(
                    connectionString,

                    new DocumentLock
                    {
                        DocumentID =
                            dto.DocumentID,

                        LockedBy =
                            userId,

                        LockType =
                            "CHECKOUT",

                        IsLocked = true
                    });

            await _repository
                .UpdateDocumentCheckOutAsync(
                    connectionString,
                    dto.DocumentID,
                    userId);
        }

        public async Task CheckInAsync(
            CheckInDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .CheckInAsync(
                    connectionString,
                    dto.DocumentID);

            await _repository
                .RemoveLockAsync(
                    connectionString,
                    dto.DocumentID);
        }
    }
}
