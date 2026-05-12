using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.BLL.Security;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    public class ServerService : IServerService
    {
        private readonly IServerRepository _repository;

        private readonly PermissionService _permissionService;

        public ServerService(
            IServerRepository repository,
            PermissionService permissionService)
        {
            _repository = repository;
            _permissionService = permissionService;
        }

        public async Task<int> CreateAsync(
            CreateServerDto dto,
            int userId,
            int roleId)
        {
            var hasPermission = await _permissionService
                .HasServerPermission(roleId, 1, "CanCreate");

            if (!hasPermission)
                throw new Exception("Permission denied");

            var entity = new Server
            {
                ServerName = dto.ServerName,
                ServerIP = dto.ServerIP,
                DatabaseServer = dto.DatabaseServer,
                StorageRootPath = dto.StorageRootPath,
                CreatedBy = userId,
                IsActive = true
            };

            return await _repository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(Server model, int roleId)
        {
            var hasPermission = await _permissionService
                .HasServerPermission(roleId, model.ServerID, "CanEdit");

            if (!hasPermission)
                throw new Exception("Permission denied");

            return await _repository.UpdateAsync(model);
        }

        public async Task<int> DeleteAsync(int serverId, int roleId)
        {
            var hasPermission = await _permissionService
                .HasServerPermission(roleId, serverId, "CanDelete");

            if (!hasPermission)
                throw new Exception("Permission denied");

            return await _repository.DeleteAsync(serverId);
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
