using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IBackgroundJobRepository
    {
        Task<int> CreateAsync(
            BackgroundJob model);

        Task<List<BackgroundJob>>
            GetPendingJobsAsync();

        Task UpdateStatusAsync(
            int jobId,
            string status,
            string error = null);
    }
}
