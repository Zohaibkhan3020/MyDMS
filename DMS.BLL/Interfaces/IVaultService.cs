using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IVaultService
    {
        Task<int> CreateAsync(CreateVaultDto dto,int userId, int roleId);
        Task<IEnumerable<Vault>> GetAllAsync(int ServerID);
    }
}
