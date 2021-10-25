using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class RoleSpec : Specification<Role>
    {
        public RoleSpec()
        {
            Query.OrderByDescending(x => x.Id);
        }

        public RoleSpec(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Query.Where(x => x.Name.Contains(filter));
            }

            Query.OrderByDescending(x => x.Id);
        }

        public RoleSpec(string filter, int skip, int take)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Query.Where(x => x.Name.Contains(filter));
            }

            Query.OrderByDescending(x => x.Id).Skip(skip).Take(take);
        }
    }
}