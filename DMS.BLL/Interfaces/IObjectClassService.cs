using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IObjectClassService
    {
        Task<int> CreateAsync(
            CreateObjectClassDto dto,
            int userId);

        Task UpdateAsync(
            UpdateObjectClassDto dto);

        Task DeleteAsync(
            int vaultId,
            int classId);

        Task<List<ObjectClass>>
            GetAllAsync(
                int vaultId);

        Task<List<ObjectClass>>
            GetByObjectTypeAsync(
                int vaultId,
                int objectTypeId);

        Task<ObjectClass>
            GetByIdAsync(
                int vaultId,
                int classId);
    }
}
