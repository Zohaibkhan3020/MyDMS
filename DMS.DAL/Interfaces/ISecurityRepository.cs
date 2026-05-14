using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface ISecurityRepository
    {
        Task AddObjectPermissionAsync(
            string connectionString,
            ObjectPermission model);

        Task AddDocumentPermissionAsync(
            string connectionString,
            DocumentPermission model);

        Task AddPropertyPermissionAsync(
            string connectionString,
            PropertyPermission model);

        Task AddWorkflowPermissionAsync(
            string connectionString,
            WorkflowRolePermission model);

        Task<bool> HasDocumentViewAccessAsync(
            string connectionString,
            int documentId,
            int userId,
            int roleId);
    }
}
