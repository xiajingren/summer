using System;
using System.Collections.Generic;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string PasswordHash { get; private set; }

        public string SecurityStamp { get; private set; }
        

        private readonly List<UserRole> _roles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();


        private readonly List<UserPermission> _userPermissions = new List<UserPermission>();
        public IReadOnlyCollection<UserPermission> UserPermissions => _userPermissions.AsReadOnly();

        private User()
        {
            // required by EF
        }

        internal User(string userName, string passwordHash)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            SecurityStamp = Guid.NewGuid().ToString("N");
        }

        public void SetRoles(IEnumerable<int> roleIds = null)
        {
            _roles.Clear();

            if (roleIds == null)
            {
                return;
            }

            foreach (var roleId in roleIds)
            {
                _roles.Add(new UserRole(Id, roleId));
            }
        }
    }
}