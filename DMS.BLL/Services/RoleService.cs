using DMS.BLL.DTOs;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.BLL.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(
            IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Role>>
            GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int>
            CreateAsync(CreateRoleDto dto)
        {
            var entity = new Role
            {
                RoleName = dto.RoleName,
                Description = dto.Description
            };

            return await _repository
                .InsertAsync(entity);
        }
    }
}
