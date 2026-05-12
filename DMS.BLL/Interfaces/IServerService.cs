using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IServerService
    {
        Task<int> CreateAsync(CreateServerDto dto, int userId, int roleId);

        Task<int> UpdateAsync(Server model, int roleId);

        Task<int> DeleteAsync(int serverId, int roleId);

        Task<IEnumerable<Server>> GetAllAsync();
    }
}
