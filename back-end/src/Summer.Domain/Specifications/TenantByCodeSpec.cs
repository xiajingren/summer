using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class TenantByCodeSpec : Specification<Tenant>, ISingleResultSpecification
    {
        public TenantByCodeSpec(string code)
        {
            Query.Where(x => x.Code == code);
        }
    }
}