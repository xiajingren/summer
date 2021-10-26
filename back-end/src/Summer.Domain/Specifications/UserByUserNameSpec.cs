using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class UserByUserNameSpec : Specification<User>, ISingleResultSpecification
    {
        public UserByUserNameSpec(string userName)
        {
            Query.Where(x => x.NormalizedUserName== userName.ToUpperInvariant());
        }
    }
}