using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.Options;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Domain.Services
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _useRepository;
        private readonly IPasswordHashService _passwordHasher;
        private readonly UserOptions _userOptions;

        public UserManager(IRepository<User> useRepository, IPasswordHashService passwordHasher,
            IOptions<UserOptions> userOptions)
        {
            _useRepository = useRepository;
            _passwordHasher = passwordHasher;
            _userOptions = userOptions.Value;
        }

        public async Task<User> CreateAsync(string userName, string password, IEnumerable<int> roleIds = null)
        {
            if (!ValidateUserName(userName))
            {
                throw new BusinessException("用户名格式不符");
            }

            if (!ValidatePassword(password))
            {
                throw new BusinessException("密码格式不符");
            }

            var storedUser = await _useRepository.GetBySpecAsync(new UserByUserNameSpec(userName));
            if (storedUser != null)
            {
                throw new BusinessException("用户已存在");
            }

            var user = new User(userName);
            user.SetRoles(roleIds);
            user.SetPasswordHash(_passwordHasher.Hash(user, password));

            return await _useRepository.AddAsync(user);
        }

        public async Task UpdateAsync(User user, string userName, string password)
        {
            if (!ValidateUserName(userName))
            {
                throw new BusinessException("用户名格式不符");
            }

            if (!ValidatePassword(password))
            {
                throw new BusinessException("密码格式不符");
            }

            var storedUser = await _useRepository.GetBySpecAsync(new UserByUserNameSpec(userName));
            if (storedUser != null && storedUser.Id != user.Id)
            {
                throw new BusinessException("用户已存在");
            }

            user.SetUserName(userName);
            if (!string.IsNullOrEmpty(password))
            {
                user.RefreshSecurityStamp();
                user.UpdatePasswordHash(_passwordHasher.Hash(user, password));
            }

            await _useRepository.UpdateAsync(user);
        }

        public bool CheckPassword(User user, string password)
        {
            return _passwordHasher.Verify(user, password);
        }

        public bool ValidateUserName(string userName)
        {
            if (userName.Length < _userOptions.UserNameRequiredLength)
            {
                return false;
            }

            return true;
        }

        public bool ValidatePassword(string password)
        {
            if (password.Length < _userOptions.PasswordRequiredLength)
            {
                return false;
            }

            if (_userOptions.PasswordRequireDigit && !Regex.IsMatch(password, "[0-9]"))
            {
                return false;
            }

            if (_userOptions.PasswordRequireLowercase && !Regex.IsMatch(password, "[a-z]"))
            {
                return false;
            }

            if (_userOptions.PasswordRequireUppercase && !Regex.IsMatch(password, "[A-Z]"))
            {
                return false;
            }

            return true;
        }
    }
}