using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface ICheckInOutRepository
    {
        Task<bool> IsCheckedOutAsync(
            string connectionString,
            int documentId);

        Task CheckOutAsync(
            string connectionString,
            DocumentCheckout model);

        Task CreateLockAsync(
            string connectionString,
            DocumentLock model);

        Task UpdateDocumentCheckOutAsync(
            string connectionString,
            int documentId,
            int userId);

        Task CheckInAsync(
            string connectionString,
            int documentId);

        Task RemoveLockAsync(
            string connectionString,
            int documentId);
    }
}
