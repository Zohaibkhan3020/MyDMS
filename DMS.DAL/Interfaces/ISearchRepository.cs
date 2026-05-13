using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DMS.Domain.Entities;

namespace DMS.DAL.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<SearchResult>>
            SearchAsync(
                string connectionString,
                string keyword);
    }
}
