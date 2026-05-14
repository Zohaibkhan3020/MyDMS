using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IFullTextSearchService
    {
        Task BuildIndexAsync(int vaultId,int documentId);

        Task<List<SearchResponse>>SearchAsync(SearchRequestDto dto);
    }
}
