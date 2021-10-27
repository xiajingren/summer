using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class RefreshTokenByUserIdSpec : Specification<RefreshToken>
    {
        public RefreshTokenByUserIdSpec(int userId)
        {
            Query.Where(x => x.UserId == userId);
        }

        public RefreshTokenByUserIdSpec(int userId, bool invalidated)
        {
            Query.Where(x => x.Invalidated == invalidated);
        }
    }
}