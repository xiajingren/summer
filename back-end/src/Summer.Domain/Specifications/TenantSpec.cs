using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class TenantSpec : Specification<Tenant>
    {
        public TenantSpec()
        {
            Query.OrderByDescending(x => x.Id);
        }

        public TenantSpec(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Query.Where(x => x.Code.Contains(filter) || x.Name.Contains(filter) || x.Host.Contains(filter));
            }

            Query.OrderByDescending(x => x.Id);
        }

        public TenantSpec(string filter, int skip, int take)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Query.Where(x => x.Code.Contains(filter) || x.Name.Contains(filter) || x.Host.Contains(filter));
            }

            Query.OrderByDescending(x => x.Id).Skip(skip).Take(take);
        }
    }
}