
using BCrypt.Net;
using DMS.BLL.DTOs;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.BLL.Services
{

    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int>CreateAsync(CreateUserDto dto)
        {
            var entity = new User
            {
                FullName = dto.FullName,

                Username = dto.Username,

                Email = dto.Email,

                PasswordHash =  BCrypt.Net.BCrypt.HashPassword(dto.Password),

                IsActive = true,

                CreatedOn = DateTime.Now,

               //CreatedBy = currentUserId,

                FailedLoginAttempts = 0,

                IsLocked = false
            };

            return await _repository.InsertAsync(entity);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }


    }
}
