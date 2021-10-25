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
        private readonly IPasswordHashService _passwordHasher;

        public UserManager(IRepository<User> useRepository, IPasswordHashService passwordHasher)
        {
            _useRepository = useRepository ?? throw new ArgumentNullException(nameof(useRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<User> CreateAsync(string userName, string password)
        {
            var storedUser = await _useRepository.GetBySpecAsync(new UserByUserNameSpec(userName));
            if (storedUser != null)
            {
                throw new BusinessException("用户已存在");
            }

            var user = new User(userName);
            user.SetPasswordHash(_passwordHasher.Hash(user, password));
            
            return await _useRepository.AddAsync(user);
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
                user.RefreshSecurityStamp();
                user.SetPasswordHash(_passwordHasher.Hash(user, password));
            }

            await _useRepository.UpdateAsync(user);
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            return Task.FromResult(_passwordHasher.Verify(user, password));
        }
    }
}