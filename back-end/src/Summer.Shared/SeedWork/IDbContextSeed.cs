using System.Threading.Tasks;

namespace Summer.Shared.SeedWork
{
    public interface IDbContextSeed
    {
        Task SeedAsync();
    }
}