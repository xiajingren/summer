using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;

namespace Summer.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository : EfRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(SummerDbContext dbContext) : base(dbContext)
        {
        }

        public Task<RefreshToken> GetByTokenAsync(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}