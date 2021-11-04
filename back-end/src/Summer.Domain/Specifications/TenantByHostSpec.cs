using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class TenantByHostSpec : Specification<Tenant>, ISingleResultSpecification
    {
        public TenantByHostSpec(string host)
        {
            Query.Where(x => x.Host == host);
        }
    }
}