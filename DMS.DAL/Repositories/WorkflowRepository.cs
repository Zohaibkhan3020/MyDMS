
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{

    public class WorkflowRepository
        : IWorkflowRepository
    {
        public async Task<int>
            CreateWorkflowAsync(
                string connectionString,
                Workflow workflow)
        {
            var sql = @"

INSERT INTO WORKFLOWS
(
    WorkflowName,
    Description,
    IsActive
)
VALUES
(
    @WorkflowName,
    @Description,
    1
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    workflow);
        }

        public async Task<int>
            CreateStateAsync(
                string connectionString,
                WorkflowState state)
        {
            var sql = @"

INSERT INTO WORKFLOW_STATES
(
    WorkflowID,
    StateName,
    StateColor,
    IsInitial,
    IsFinal,
    SortOrder
)
VALUES
(
    @WorkflowID,
    @StateName,
    @StateColor,
    @IsInitial,
    @IsFinal,
    @SortOrder
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    state);
        }

        public async Task<int>
            CreateTransitionAsync(
                string connectionString,
                WorkflowTransition transition)
        {
            var sql = @"

INSERT INTO WORKFLOW_TRANSITIONS
(
    WorkflowID,
    FromStateID,
    ToStateID,
    ActionName,
    AllowedRoleID
)
VALUES
(
    @WorkflowID,
    @FromStateID,
    @ToStateID,
    @ActionName,
    @AllowedRoleID
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    transition);
        }

        public async Task<WorkflowTransition>
            GetTransitionAsync(
                string connectionString,
                int transitionId)
        {
            var sql = @"

SELECT *
FROM WORKFLOW_TRANSITIONS
WHERE TransitionID = @transitionId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .QueryFirstOrDefaultAsync<WorkflowTransition>(
                    sql,
                    new { transitionId });
        }

        public async Task<DocumentWorkflow>
            GetDocumentWorkflowAsync(
                string connectionString,
                int documentId)
        {
            var sql = @"

SELECT *
FROM DOCUMENT_WORKFLOW
WHERE DocumentID = @documentId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .QueryFirstOrDefaultAsync<DocumentWorkflow>(
                    sql,
                    new { documentId });
        }

        public async Task StartWorkflowAsync(
            string connectionString,
            DocumentWorkflow model)
        {
            var sql = @"

INSERT INTO DOCUMENT_WORKFLOW
(
    DocumentID,
    WorkflowID,
    CurrentStateID,
    StartedBy,
    Completed
)
VALUES
(
    @DocumentID,
    @WorkflowID,
    @CurrentStateID,
    @StartedBy,
    0
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    model);
        }

        public async Task MoveNextStateAsync(
            string connectionString,
            int documentWorkflowId,
            int nextStateId)
        {
            var sql = @"

UPDATE DOCUMENT_WORKFLOW
SET CurrentStateID = @nextStateId
WHERE DocumentWorkflowID = @documentWorkflowId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    new
                    {
                        documentWorkflowId,
                        nextStateId
                    });
        }

        public async Task AddHistoryAsync(
            string connectionString,
            WorkflowHistory history)
        {
            var sql = @"

INSERT INTO DOCUMENT_WORKFLOW_HISTORY
(
    DocumentID,
    FromStateID,
    ToStateID,
    ActionName,
    ActionBy,
    Comments
)
VALUES
(
    @DocumentID,
    @FromStateID,
    @ToStateID,
    @ActionName,
    @ActionBy,
    @Comments
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    history);
        }
    }
}
