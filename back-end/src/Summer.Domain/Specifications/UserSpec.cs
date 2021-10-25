using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class UserSpec : Specification<User>
    {
        public UserSpec()
        {
            Query.OrderByDescending(x => x.Id);
        }

        public UserSpec(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Query.Where(x => x.UserName.Contains(filter));
            }

            Query.OrderByDescending(x => x.Id);
            
            Query.Include(x => x.Roles);
        }

        public UserSpec(string filter, int skip, int take)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Query.Where(x => x.UserName.Contains(filter));
            }

            Query.OrderByDescending(x => x.Id).Skip(skip).Take(take);

            Query.Include(x => x.Roles);
        }
    }
}