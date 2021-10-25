using System;
using System.Threading.Tasks;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Domain.Services
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _useRepository;

        public UserManager(IRepository<User> useRepository)
        {
            _useRepository = useRepository ?? throw new ArgumentNullException(nameof(useRepository));
        }

        public async Task<User> CreateAsync(string userName, string password)
        {
            var user = await _useRepository.GetBySpecAsync(new UserByUserNameSpec(userName));
            if (user != null)
            {
                throw new BusinessException("用户已存在");
            }

            return await _useRepository.AddAsync(new User(userName, password, Guid.NewGuid().ToString("N")));
        }

        public async Task UpdateAsync(User user, string userName, string password)
        {
            var storedUser = await _useRepository.GetBySpecAsync(new UserByUserNameSpec(userName));
            if (storedUser != null && storedUser.Id != user.Id)
            {
                throw new BusinessException("用户已存在");
            }

            user.UserName = userName;
            if (!string.IsNullOrEmpty(password))
            {
                user.PasswordHash = password;
                user.SecurityStamp = Guid.NewGuid().ToString("N");
            }

            await _useRepository.UpdateAsync(user);
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            return Task.FromResult(user.PasswordHash == password);
        }
    }
}