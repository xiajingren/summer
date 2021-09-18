using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure.IdentityServer
{
    public class PersistedGrantDbContextSeed : IDbContextSeed
    {
        private readonly PersistedGrantDbContext _context;

        public PersistedGrantDbContextSeed(PersistedGrantDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}