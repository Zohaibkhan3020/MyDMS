using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DMS.BLL.Services
{
    
    public class WorkflowService
        : IWorkflowService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly IWorkflowRepository
            _repository;

        public WorkflowService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            IWorkflowRepository repository)
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
                    .GetByIdAsync(
                        vaultId);

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
            CreateWorkflowAsync(
                CreateWorkflowDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            return await _repository
                .CreateWorkflowAsync(
                    connectionString,

                    new Workflow
                    {
                        WorkflowName =
                            dto.WorkflowName,

                        Description =
                            dto.Description,

                        IsActive = true
                    });
        }

        public async Task<int>
            CreateStateAsync(
                CreateStateDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            return await _repository
                .CreateStateAsync(
                    connectionString,

                    new WorkflowState
                    {
                        WorkflowID =
                            dto.WorkflowID,

                        StateName =
                            dto.StateName,

                        StateColor =
                            dto.StateColor,

                        IsInitial =
                            dto.IsInitial,

                        IsFinal =
                            dto.IsFinal,

                        SortOrder =
                            dto.SortOrder
                    });
        }

        public async Task<int>
            CreateTransitionAsync(
                CreateTransitionDto dto)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            return await _repository
                .CreateTransitionAsync(
                    connectionString,

                    new WorkflowTransition
                    {
                        WorkflowID =
                            dto.WorkflowID,

                        FromStateID =
                            dto.FromStateID,

                        ToStateID =
                            dto.ToStateID,

                        ActionName =
                            dto.ActionName,

                        AllowedRoleID =
                            dto.AllowedRoleID
                    });
        }

        public async Task StartWorkflowAsync(
            StartWorkflowDto dto,
            int userId)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            await _repository
                .StartWorkflowAsync(
                    connectionString,

                    new DocumentWorkflow
                    {
                        DocumentID =
                            dto.DocumentID,

                        WorkflowID =
                            dto.WorkflowID,

                        CurrentStateID =
                            dto.InitialStateID,

                        StartedBy =
                            userId,

                        Completed = false
                    });
        }

        public async Task MoveNextAsync(
            WorkflowActionDto dto,
            int userId)
        {
            var connectionString =
                await BuildVaultConnection(
                    dto.VaultID);

            var transition =
                await _repository
                    .GetTransitionAsync(
                        connectionString,
                        dto.TransitionID);

            if (transition == null)
            {
                throw new Exception(
                    "Transition not found");
            }

            var documentWorkflow =
                await _repository
                    .GetDocumentWorkflowAsync(
                        connectionString,
                        dto.DocumentID);

            await _repository
                .MoveNextStateAsync(
                    connectionString,

                    documentWorkflow.DocumentWorkflowID,

                    transition.ToStateID);

            await _repository
                .AddHistoryAsync(
                    connectionString,

                    new WorkflowHistory
                    {
                        DocumentID =
                            dto.DocumentID,

                        FromStateID =
                            transition.FromStateID,

                        ToStateID =
                            transition.ToStateID,

                        ActionName =
                            transition.ActionName,

                        ActionBy =
                            userId,

                        Comments =
                            dto.Comments
                    });
        }
    }
}
