using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IVaultRepository
    {
        Task<int> InsertAsync(Vault model);
        Task<Vault> GetByIdAsync(int vaultId);
    }
}
