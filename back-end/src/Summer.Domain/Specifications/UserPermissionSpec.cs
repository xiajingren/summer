using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class UserPermissionSpec : Specification<UserPermission, string>
    {
        public UserPermissionSpec(int userId)
        {
            Query.Where(x => x.UserId == userId);
            Query.Select(x => x.PermissionCode);
        }
    }
}