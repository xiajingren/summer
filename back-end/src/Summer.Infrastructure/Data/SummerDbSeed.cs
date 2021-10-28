using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.Application.Permissions;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.Data
{
    public class SummerDbSeed : IDataSeed
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly SummerDbContext _dbContext;

        public SummerDbSeed(IUserManager userManager, IRoleManager roleManager, IRepository<User> userRepository,
            IRepository<Permission> permissionRepository,SummerDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            await _dbContext.Database.MigrateAsync();
            
            if (await _dbContext.Users.AnyAsync())
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