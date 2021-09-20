using System.Threading.Tasks;

namespace Summer.Infra.Data.SeedWork
{
    public interface IDbContextSeed
    {
        Task SeedAsync();
    }
}