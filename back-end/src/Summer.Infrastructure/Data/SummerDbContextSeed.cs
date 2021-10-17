using System.Threading.Tasks;
using Summer.Shared.SeedWork;

namespace Summer.Infrastructure.Data
{
    public class SummerDbContextSeed : IDbContextSeed
    {
        public async Task SeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}