using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IAIMetadataRepository
    {
        Task SaveMetadataAsync(
            string connectionString,
            AIMetadata model);

        Task SaveTagAsync(
            string connectionString,
            AITag model);
    }
}
