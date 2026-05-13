using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IDynamicRecordService
    {
        Task<int> SaveAsync(
            SaveDynamicRecordDto dto,
            int userId);

        Task<List<DynamicRecordDto>>
    GetRecordsAsync(
        int vaultId,
        int objectTypeId);
    }
}
