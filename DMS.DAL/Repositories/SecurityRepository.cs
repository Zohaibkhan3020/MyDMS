
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    public class SecurityRepository
        : ISecurityRepository
    {
        public async Task AddObjectPermissionAsync(
            string connectionString,
            ObjectPermission model)
        {
            var sql = @"

INSERT INTO OBJECT_PERMISSIONS
(
    ObjectTypeID,
    RoleID,
    CanView,
    CanCreate,
    CanEdit,
    CanDelete
)
VALUES
(
    @ObjectTypeID,
    @RoleID,
    @CanView,
    @CanCreate,
    @CanEdit,
    @CanDelete
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                model);
        }

        public async Task AddDocumentPermissionAsync(
            string connectionString,
            DocumentPermission model)
        {
            var sql = @"

INSERT INTO DOCUMENT_PERMISSIONS
(
    DocumentID,
    UserID,
    RoleID,
    CanView,
    CanEdit,
    CanDelete,
    CanDownload
)
VALUES
(
    @DocumentID,
    @UserID,
    @RoleID,
    @CanView,
    @CanEdit,
    @CanDelete,
    @CanDownload
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                model);
        }

        public async Task AddPropertyPermissionAsync(
            string connectionString,
            PropertyPermission model)
        {
            var sql = @"

INSERT INTO PROPERTY_PERMISSIONS
(
    PropertyID,
    RoleID,
    CanView,
    CanEdit
)
VALUES
(
    @PropertyID,
    @RoleID,
    @CanView,
    @CanEdit
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                model);
        }

        public async Task AddWorkflowPermissionAsync(
            string connectionString,
            WorkflowRolePermission model)
        {
            var sql = @"

INSERT INTO WORKFLOW_ROLE_PERMISSIONS
(
    WorkflowID,
    StateID,
    RoleID,
    CanApprove,
    CanReject
)
VALUES
(
    @WorkflowID,
    @StateID,
    @RoleID,
    @CanApprove,
    @CanReject
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                model);
        }

        public async Task<bool>
            HasDocumentViewAccessAsync(
                string connectionString,
                int documentId,
                int userId,
                int roleId)
        {
            var sql = @"

SELECT COUNT(1)
FROM DOCUMENT_PERMISSIONS
WHERE DocumentID = @documentId
AND
(
    UserID = @userId
    OR RoleID = @roleId
)
AND CanView = 1

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var count =
                await connection
                    .ExecuteScalarAsync<int>(
                        sql,
                        new
                        {
                            documentId,
                            userId,
                            roleId
                        });

            return count > 0;
        }
    }
}
