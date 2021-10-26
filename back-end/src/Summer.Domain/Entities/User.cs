using System;
using System.Collections.Generic;
using System.Linq;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string NormalizedUserName { get; private set; }

        public string PasswordHash { get; private set; }

        public string SecurityStamp { get; private set; }

        private readonly List<UserRole> _roles = new();
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();
        public IEnumerable<int> RoleIds => Roles.Select(x => x.RoleId);
        
        private User()
        {
            // required by EF
        }

        internal User(string userName)
        {
            SetUserName(userName);
            RefreshSecurityStamp();
        }

        internal void RefreshSecurityStamp()
        {
            SecurityStamp = Guid.NewGuid().ToString("N");
        }

        internal void SetUserName(string userName)
        {
            UserName = userName;
            NormalizedUserName = UserName.ToUpper();
        }

        internal void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        internal void UpdatePasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;

            // todo: UpdatePassword Event
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