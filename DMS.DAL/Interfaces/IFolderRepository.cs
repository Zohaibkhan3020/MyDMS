using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IFolderRepository
    {
        Task<int> CreateFolderAsync(
            string connectionString,
            Folder folder);

        Task<Folder> GetFolderAsync(
            string connectionString,
            int folderId);

        Task<List<Folder>> GetAllFoldersAsync(
            string connectionString);

        Task UpdateFolderPathAsync(
            string connectionString,
            int folderId,
            string path,
            int level);

        Task MoveFolderAsync(
            string connectionString,
            int folderId,
            int? parentFolderId);
    }
}
