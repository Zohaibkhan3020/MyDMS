
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{

    public class ObjectClassRepository
        : IObjectClassRepository
    {
        public async Task<int>
            InsertAsync(
                string connectionString,
                ObjectClass model)
        {
            var sql = @"

INSERT INTO OBJECT_CLASSES
(
    ObjectTypeID,
    ClassName,
    Description,
    IconName,
    ColorCode,
    IsActive,
    CreatedBy
)
VALUES
(
    @ObjectTypeID,
    @ClassName,
    @Description,
    @IconName,
    @ColorCode,
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
            ObjectClass model)
        {
            var sql = @"

UPDATE OBJECT_CLASSES
SET

    ObjectTypeID = @ObjectTypeID,

    ClassName = @ClassName,

    Description = @Description,

    IconName = @IconName,

    ColorCode = @ColorCode,

    IsActive = @IsActive

WHERE ClassID = @ClassID

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
            int classId)
        {
            var sql = @"

DELETE FROM OBJECT_CLASSES
WHERE ClassID = @classId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                new { classId });
        }

        public async Task<List<ObjectClass>>
            GetAllAsync(
                string connectionString)
        {
            var sql = @"

SELECT *
FROM OBJECT_CLASSES
ORDER BY ClassID DESC

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection.QueryAsync<ObjectClass>(
                    sql);

            return data.ToList();
        }

        public async Task<List<ObjectClass>>
            GetByObjectTypeAsync(
                string connectionString,
                int objectTypeId)
        {
            var sql = @"

SELECT *
FROM OBJECT_CLASSES
WHERE ObjectTypeID = @objectTypeId

ORDER BY ClassName

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection.QueryAsync<ObjectClass>(
                    sql,
                    new { objectTypeId });

            return data.ToList();
        }

        public async Task<ObjectClass>
            GetByIdAsync(
                string connectionString,
                int classId)
        {
            var sql = @"

SELECT *
FROM OBJECT_CLASSES
WHERE ClassID = @classId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .QueryFirstOrDefaultAsync<ObjectClass>(
                    sql,
                    new { classId });
        }
    }
}
