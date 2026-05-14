using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IDigitalSignatureRepository
    {
        Task SaveSignatureAsync(
            string connectionString,
            DocumentSignature model);

        Task SaveWatermarkAsync(
            string connectionString,
            DocumentWatermark model);
    }
}
