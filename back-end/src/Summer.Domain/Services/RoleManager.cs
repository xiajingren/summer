using System;
using System.Threading.Tasks;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Domain.Services
{
    public class RoleManager : IRoleManager
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleManager(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<Role> CreateAsync(string name)
        {
            var role = await _roleRepository.GetBySpecAsync(new RoleByNameSpec(name));
            if (role != null)
            {
                throw new BusinessException("角色已存在");
            }

            return await _roleRepository.AddAsync(new Role(name));
        }

        public async Task UpdateAsync(Role role, string name)
        {
            var storedRole = await _roleRepository.GetBySpecAsync(new RoleByNameSpec(name));
            if (storedRole != null && storedRole.Id != role.Id)
            {
                throw new BusinessException("角色已存在");
            }

            role.SetName(name);

            await _roleRepository.UpdateAsync(role);
        }
    }
}