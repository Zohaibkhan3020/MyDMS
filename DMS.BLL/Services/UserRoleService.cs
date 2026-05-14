using DMS.BLL.DTOs;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.BLL.Services
{
    public class UserRoleService
    {
        private readonly IUserRoleRepository _repository;

        public UserRoleService(
            IUserRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<int>
            CreateAsync(CreateUserRoleDto dto)
        {
            var entity = new UserRole
            {
                UserID = dto.UserID,
                RoleID = dto.RoleID
            };

            return await _repository.InsertAsync(entity);
        }
    }
}
