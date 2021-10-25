using System;
using System.Collections.Generic;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string UserName { get; internal set; }

        public string PasswordHash { get; private set; }

        public string SecurityStamp { get; private set; }


        private readonly List<UserRole> _roles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

        private User()
        {
            // required by EF
        }

        internal User(string userName)
        {
            UserName = userName;
            RefreshSecurityStamp();
        }

        internal void RefreshSecurityStamp()
        {
            SecurityStamp = Guid.NewGuid().ToString("N");
        }

        internal void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
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