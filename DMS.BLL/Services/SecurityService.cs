using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DMS.BLL.Services
{
    

    public class SecurityService
        : ISecurityService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly ISecurityRepository
            _repository;

        public SecurityService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            ISecurityRepository repository)
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

        public async Task AddObjectPermissionAsync(
            ObjectPermissionDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .AddObjectPermissionAsync(
                    connectionString,

                    new ObjectPermission
                    {
                        ObjectTypeID =
                            dto.ObjectTypeID,

                        RoleID =
                            dto.RoleID,

                        CanView =
                            dto.CanView,

                        CanCreate =
                            dto.CanCreate,

                        CanEdit =
                            dto.CanEdit,

                        CanDelete =
                            dto.CanDelete
                    });
        }

        public async Task AddDocumentPermissionAsync(
            DocumentPermissionDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .AddDocumentPermissionAsync(
                    connectionString,

                    new DocumentPermission
                    {
                        DocumentID =
                            dto.DocumentID,

                        UserID =
                            dto.UserID,

                        RoleID =
                            dto.RoleID,

                        CanView =
                            dto.CanView,

                        CanEdit =
                            dto.CanEdit,

                        CanDelete =
                            dto.CanDelete,

                        CanDownload =
                            dto.CanDownload
                    });
        }

        public async Task AddPropertyPermissionAsync(
            PropertyPermissionDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .AddPropertyPermissionAsync(
                    connectionString,

                    new PropertyPermission
                    {
                        PropertyID =
                            dto.PropertyID,

                        RoleID =
                            dto.RoleID,

                        CanView =
                            dto.CanView,

                        CanEdit =
                            dto.CanEdit
                    });
        }

        public async Task AddWorkflowPermissionAsync(
            WorkflowPermissionDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .AddWorkflowPermissionAsync(
                    connectionString,

                    new WorkflowRolePermission
                    {
                        WorkflowID =
                            dto.WorkflowID,

                        StateID =
                            dto.StateID,

                        RoleID =
                            dto.RoleID,

                        CanApprove =
                            dto.CanApprove,

                        CanReject =
                            dto.CanReject
                    });
        }

        public async Task<bool>
            HasDocumentAccessAsync(
                int vaultId,
                int documentId,
                int userId,
                int roleId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            return await _repository
                .HasDocumentViewAccessAsync(
                    connectionString,
                    documentId,
                    userId,
                    roleId);
        }
    }
}
