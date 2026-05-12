using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IObjectTypeService
    {
        Task<int> CreateAsync(
            CreateObjectTypeDto dto,
            int userId);

        Task UpdateAsync(
            UpdateObjectTypeDto dto);

        Task DeleteAsync(
            int vaultId,
            int objectTypeId);

        Task<List<ObjectType>>
            GetAllAsync(
                int vaultId);

        Task<ObjectType>
            GetByIdAsync(
                int vaultId,
                int objectTypeId);
    }
}
