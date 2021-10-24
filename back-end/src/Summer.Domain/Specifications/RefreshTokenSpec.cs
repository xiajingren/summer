using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class RefreshTokenSpec : Specification<RefreshToken>, ISingleResultSpecification
    {
        public RefreshTokenSpec(string refreshToken)
        {
            Query.Where(x => x.Token == refreshToken);
        }
    }
}