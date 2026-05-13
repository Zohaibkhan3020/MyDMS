using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IWorkflowRepository
    {
        Task<int> CreateWorkflowAsync(
            string connectionString,
            Workflow workflow);

        Task<int> CreateStateAsync(
            string connectionString,
            WorkflowState state);

        Task<int> CreateTransitionAsync(
            string connectionString,
            WorkflowTransition transition);

        Task<WorkflowTransition>
            GetTransitionAsync(
                string connectionString,
                int transitionId);

        Task<DocumentWorkflow>
            GetDocumentWorkflowAsync(
                string connectionString,
                int documentId);

        Task StartWorkflowAsync(
            string connectionString,
            DocumentWorkflow model);

        Task MoveNextStateAsync(
            string connectionString,
            int documentWorkflowId,
            int nextStateId);

        Task AddHistoryAsync(
            string connectionString,
            WorkflowHistory history);
    }
}
