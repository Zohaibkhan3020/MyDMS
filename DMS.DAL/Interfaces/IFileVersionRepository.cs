using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IFileVersionRepository
    {
        Task<int>
            GetNextVersionAsync(
                string connectionString,
                int fileId);

        Task InsertAsync(
            string connectionString,
            FileVersion version);

        Task<List<FileVersion>>
            GetVersionsAsync(
                string connectionString,
                int fileId);

        Task ResetCurrentVersionAsync(
            string connectionString,
            int fileId);
    }
}
