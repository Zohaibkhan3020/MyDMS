using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface ISecurityService
    {
        Task AddObjectPermissionAsync(
            ObjectPermissionDto dto);

        Task AddDocumentPermissionAsync(
            DocumentPermissionDto dto);

        Task AddPropertyPermissionAsync(
            PropertyPermissionDto dto);

        Task AddWorkflowPermissionAsync(
            WorkflowPermissionDto dto);

        Task<bool> HasDocumentAccessAsync(
            int vaultId,
            int documentId,
            int userId,
            int roleId);
    }
}
