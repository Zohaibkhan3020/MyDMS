using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.DAL.Repositories
{
    public class ObjectPropertyRepository
        : IObjectPropertyRepository
    {
        public async Task<int>
            InsertAsync(
                string connectionString,
                ObjectProperty model)
        {
            var sql = @"

INSERT INTO OBJECT_PROPERTIES
(
    ObjectTypeID,
    PropertyName,
    DisplayName,
    DataType,
    ControlType,
    IsRequired,
    IsUnique,
    IsSearchable,
    IsSystem,
    DefaultValue,
    LookupObjectTypeID,
    SortOrder,
    IsActive
)
VALUES
(
    @ObjectTypeID,
    @PropertyName,
    @DisplayName,
    @DataType,
    @ControlType,
    @IsRequired,
    @IsUnique,
    @IsSearchable,
    0,
    @DefaultValue,
    @LookupObjectTypeID,
    @SortOrder,
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
                    model);
        }

        public async Task UpdateAsync(
            string connectionString,
            ObjectProperty model)
        {
            var sql = @"

UPDATE OBJECT_PROPERTIES
SET

    ObjectTypeID = @ObjectTypeID,

    PropertyName = @PropertyName,

    DisplayName = @DisplayName,

    DataType = @DataType,

    ControlType = @ControlType,

    IsRequired = @IsRequired,

    IsUnique = @IsUnique,

    IsSearchable = @IsSearchable,

    DefaultValue = @DefaultValue,

    LookupObjectTypeID =
        @LookupObjectTypeID,

    SortOrder = @SortOrder,

    IsActive = @IsActive

WHERE PropertyID = @PropertyID

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                model);
        }

        public async Task DeleteAsync(string connectionString,int propertyId)
        {
            var sql = @"

DELETE FROM OBJECT_PROPERTIES
WHERE PropertyID = @propertyId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                new { propertyId });
        }

        public async Task<List<ObjectProperty>>GetAllAsync(string connectionString)
        {
            var sql = @"

SELECT *
FROM OBJECT_PROPERTIES
ORDER BY SortOrder

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection
                    .QueryAsync<ObjectProperty>(
                        sql);

            return data.ToList();
        }

        public async Task<List<ObjectProperty>> GetByObjectTypeAsync(string connectionString,int objectTypeId, int ClassID)
        {
            var sql = @"
                    SELECT *
                    FROM OBJECT_PROPERTIES
                    WHERE ObjectTypeID = @objectTypeId AND ClassID = @ClassID

                    ORDER BY SortOrder

                    ";

            using var connection = new SqlConnection(connectionString);

            var data = await connection.QueryAsync<ObjectProperty>(sql,new { objectTypeId });

            return data.ToList();
        }

        public async Task<ObjectProperty>GetByIdAsync(string connectionString, int propertyId)
        {
            var sql = @"
                    SELECT * FROM OBJECT_PROPERTIES
                    WHERE PropertyID = @propertyId";

            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<ObjectProperty>(sql, new { propertyId });
        }
    }
}
