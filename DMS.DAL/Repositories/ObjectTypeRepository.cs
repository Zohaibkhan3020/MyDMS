
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{

    public class ObjectTypeRepository : IObjectTypeRepository
    {
        public async Task<int>
            InsertAsync(
                string connectionString,
                ObjectType model)
        {
            var sql = @"

INSERT INTO OBJECT_TYPES
(
    ObjectTypeName,
    TableName,
    HasFiles,
    IsSystem,
    IsActive,
    CreatedBy
)
VALUES
(
    @ObjectTypeName,
    @TableName,
    @HasFiles,
    0,
    1,
    @CreatedBy
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    model);
        }

        public async Task UpdateAsync(
            string connectionString,
            ObjectType model)
        {
            var sql = @"

UPDATE OBJECT_TYPES
SET

    ObjectTypeName = @ObjectTypeName,

    HasFiles = @HasFiles,

    IsActive = @IsActive

WHERE ObjectTypeID = @ObjectTypeID

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                model);
        }

        public async Task DeleteAsync(
            string connectionString,
            int objectTypeId)
        {
            var sql = @"

DELETE FROM OBJECT_TYPES
WHERE ObjectTypeID = @objectTypeId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                new { objectTypeId });
        }

        public async Task<List<ObjectType>>
            GetAllAsync(
                string connectionString)
        {
            var sql = @"

SELECT *
FROM OBJECT_TYPES
ORDER BY ObjectTypeID DESC

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection.QueryAsync<ObjectType>(
                    sql);

            return data.ToList();
        }

        public async Task<ObjectType>
            GetByIdAsync(
                string connectionString,
                int objectTypeId)
        {
            var sql = @"

SELECT *
FROM OBJECT_TYPES
WHERE ObjectTypeID = @objectTypeId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .QueryFirstOrDefaultAsync<ObjectType>(
                    sql,
                    new { objectTypeId });
        }
    }
}
