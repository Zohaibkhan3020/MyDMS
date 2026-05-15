using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IObjectPropertyService
    {
        Task<int> CreateAsync(CreateObjectPropertyDto dto);

        Task UpdateAsync(UpdateObjectPropertyDto dto);

        Task DeleteAsync(int vaultId,int propertyId);

        Task<List<ObjectProperty>> GetAllAsync(int vaultId);

        Task<List<ObjectProperty>> GetByObjectTypeAsync(int vaultId,int objectTypeId,int ClassID);

        Task<ObjectProperty> GetByIdAsync(int vaultId,int propertyId);
    }
}
