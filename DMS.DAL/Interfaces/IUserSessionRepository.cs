using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IUserSessionRepository
    {
        Task<int> InsertAsync(
            UserSession model);

        Task DeactivateSessions(
            int userId);
    }
}
