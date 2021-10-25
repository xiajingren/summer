using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class RefreshTokenByTokenSpec : Specification<RefreshToken>, ISingleResultSpecification
    {
        public RefreshTokenByTokenSpec(string refreshToken)
        {
            Query.Where(x => x.Token == refreshToken);
        }
    }
}