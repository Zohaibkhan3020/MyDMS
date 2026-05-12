using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<int> InsertAsync(User model);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByUsernameAsync(string username);
        Task<User> ValidateUserAsync(string username);
        Task UpdateLoginSuccess(int userId);
        Task IncreaseFailedAttempts(int userId);
    }
}
