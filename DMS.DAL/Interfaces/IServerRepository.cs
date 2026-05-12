using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IServerRepository
    {
        Task<int> InsertAsync(Server model);

        Task<int> UpdateAsync(Server model);

        Task<int> DeleteAsync(int serverId);

        Task<IEnumerable<Server>> GetAllAsync();

        Task<Server> GetByIdAsync(int serverId);
    }
}
