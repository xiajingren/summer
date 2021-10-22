using System.Collections.Generic;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class Role : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        private readonly List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        private Role()
        {
            // required by EF
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}