using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IFolderService
    {
        Task<int> CreateFolderAsync(
            CreateFolderDto dto,
            int userId);

        Task<List<FolderTreeDto>>
            GetTreeAsync(
                int vaultId);

        Task MoveFolderAsync(
            MoveFolderDto dto);
    }
}
