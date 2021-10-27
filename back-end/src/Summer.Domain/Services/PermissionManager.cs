using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Domain.Services
{
    public class PermissionManager : IPermissionManager
    {
        private readonly IReadRepository<Permission> _permissionRepository;
        private readonly IReadRepository<User> _userRepository;

        public PermissionManager(IReadRepository<Permission> permissionRepository, IReadRepository<User> userRepository)
        {
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync(int targetId, PermissionType permissionType)
        {
            return permissionType switch
            {
                PermissionType.Role => await _permissionRepository.ListAsync(new PermissionSpec(targetId,
                    PermissionType.Role)),
                PermissionType.User => await GetUserPermissionsAsync(targetId),
                _ => null
            };
        }

        private async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new BusinessException();
            }

            var userPermissions =
                await _permissionRepository.ListAsync(new PermissionSpec(user.Id, PermissionType.User));

            var rolePermissions =
                await _permissionRepository.ListAsync(new PermissionSpec(user.RoleIds, PermissionType.Role));

            userPermissions.AddRange(rolePermissions);

            return userPermissions.Distinct();
        }

        public async Task<bool> CheckUserPermissionAsync(int userId, string permissionCode)
        {
            var permissions = await GetUserPermissionsAsync(userId);

            return permissions.Select(x => x.PermissionCode).Contains(permissionCode);
        }
    }
}