using System.Threading.Tasks;

namespace Summer.Infra.Data.DataSeeds
{
    public interface IDbContextSeed
    {
        Task SeedAsync();
    }
}