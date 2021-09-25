using System.Threading.Tasks;

namespace Summer.Infra.Bootstrapper.DataSeeds
{
    public interface IDbContextSeed
    {
        Task SeedAsync();
    }
}