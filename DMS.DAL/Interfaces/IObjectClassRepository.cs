using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IObjectClassRepository
    {
        Task<int> InsertAsync(
            string connectionString,
            ObjectClass model);

        Task UpdateAsync(
            string connectionString,
            ObjectClass model);

        Task DeleteAsync(
            string connectionString,
            int classId);

        Task<List<ObjectClass>>
            GetAllAsync(
                string connectionString);

        Task<List<ObjectClass>>
            GetByObjectTypeAsync(
                string connectionString,
                int objectTypeId);

        Task<ObjectClass>
            GetByIdAsync(
                string connectionString,
                int classId);
    }
}
