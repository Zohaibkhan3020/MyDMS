
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{

    public class SearchRepository
        : ISearchRepository
    {
        public async Task<List<SearchResult>>
            SearchAsync(
                string connectionString,
                string keyword)
        {
            var sql = @"

SELECT

    D.DocumentID,

    D.Title,

    O.ObjectName AS ObjectType,

    P.DisplayName AS PropertyName,

    M.ValueText AS Value

FROM DOCUMENTS D

INNER JOIN OBJECT_TYPES O
ON D.ObjectTypeID = O.ObjectTypeID

INNER JOIN METADATA_VALUES M
ON D.DocumentID = M.DocumentID

INNER JOIN OBJECT_PROPERTIES P
ON M.PropertyID = P.PropertyID

WHERE

    D.IsDeleted = 0

AND
(
       D.Title LIKE '%' + @keyword + '%'

    OR M.ValueText LIKE '%' + @keyword + '%'
)

ORDER BY D.DocumentID DESC

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection
                    .QueryAsync<SearchResult>(
                        sql,
                        new { keyword });

            return data.ToList();
        }
    }
}
