using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class RoleByNameSpec : Specification<Role>, ISingleResultSpecification
    {
        public RoleByNameSpec(string name)
        {
            Query.Where(x => x.NormalizedName == name);
        }
    }
}