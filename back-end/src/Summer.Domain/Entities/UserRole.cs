using System.Collections.Generic;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class UserRole : ValueObject
    {
        public int UserId { get; private set; }

        public int RoleId { get; private set; }

        private UserRole()
        {
            // required by EF
        }

        public UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return RoleId;
        }
    }
}