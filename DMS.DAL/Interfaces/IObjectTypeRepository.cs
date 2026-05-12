using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IObjectTypeRepository
    {
        Task<int> InsertAsync(string connectionString, ObjectType model);

        Task UpdateAsync(string connectionString,ObjectType model);

        Task DeleteAsync(string connectionString,int objectTypeId);

        Task<List<ObjectType>> GetAllAsync(string connectionString);

        Task<ObjectType> GetByIdAsync(string connectionString, int objectTypeId);
    }
}
