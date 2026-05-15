using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IObjectPropertyRepository
    {
        Task<int> InsertAsync(string connectionString, ObjectProperty model);

        Task UpdateAsync(string connectionString,ObjectProperty model);

        Task DeleteAsync(string connectionString,int propertyId);

        Task<List<ObjectProperty>> GetAllAsync( string connectionString);

        Task<List<ObjectProperty>> GetByObjectTypeAsync(string connectionString, int objectTypeId,int ClassID);

        Task<ObjectProperty> GetByIdAsync( string connectionString,int propertyId);
    }
}
