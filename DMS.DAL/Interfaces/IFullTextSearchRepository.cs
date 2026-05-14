using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IFullTextSearchRepository
    {
        Task CreateIndexAsync(
            string connectionString,
            SearchIndex model);

        Task CreateKeywordAsync(
            string connectionString,
            SearchKeyword model);

        Task<List<SearchResponse>>
            SearchAsync(
                string connectionString,
                string keyword);
    }
}
