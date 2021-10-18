using System.Threading.Tasks;

namespace Summer.Infrastructure.SeedWork
{
    public interface IDbContextSeed
    {
        Task SeedAsync();
    }
}