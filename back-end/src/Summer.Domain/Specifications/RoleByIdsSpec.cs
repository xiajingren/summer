using System.Collections.Generic;
using System.Linq;
using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class RoleByIdsSpec : Specification<Role>
    {
        public RoleByIdsSpec(IEnumerable<int> ids)
        {
            Query.Where(x => ids.Contains(x.Id));
        }
    }
}