using System.Threading.Tasks;
using Summer.Application.Permissions;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.Data.Seeds
{
    public class UserDataSeed : IDataSeed
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Permission> _permissionRepository;

        public UserDataSeed(IUserManager userManager, IRoleManager roleManager, IRepository<User> userRepository,
            IRepository<Permission> permissionRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task SeedAsync()
        {
            if (await _userRepository.AnyAsync())
            {
                return;
            }

            var superAdminRole = await _roleManager.CreateAsync("SupperAdmin");

            foreach (var permissionInfo in PermissionHelper.Permissions)
            {
                await _permissionRepository.AddAsync(new Permission(superAdminRole.Id, PermissionType.Role,
                    permissionInfo.Code));
            }

            await _userManager.CreateAsync("admin", "123456", new[] {superAdminRole.Id});
        }
    }
}