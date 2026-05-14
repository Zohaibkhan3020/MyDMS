
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    public class FullTextSearchRepository
        : IFullTextSearchRepository
    {
        public async Task CreateIndexAsync(
            string connectionString,
            SearchIndex model)
        {
            var sql = @"

INSERT INTO SEARCH_INDEX
(
    DocumentID,
    IndexedText
)
VALUES
(
    @DocumentID,
    @IndexedText
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

        public async Task CreateKeywordAsync(
            string connectionString,
            SearchKeyword model)
        {
            var sql = @"

INSERT INTO SEARCH_KEYWORDS
(
    DocumentID,
    KeywordText
)
VALUES
(
    @DocumentID,
    @KeywordText
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

        public async Task<List<SearchResponse>>
            SearchAsync(
                string connectionString,
                string keyword)
        {
            var sql = @"

SELECT DISTINCT
    d.DocumentID,
    d.Title,
    s.IndexedText AS MatchText

FROM DOCUMENTS d

INNER JOIN SEARCH_INDEX s
ON d.DocumentID = s.DocumentID

WHERE
    s.IndexedText LIKE '%' + @keyword + '%'

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var result =
                await connection
                    .QueryAsync<SearchResponse>(
                        sql,
                        new { keyword });

            return result.ToList();
        }

        
    }
}
