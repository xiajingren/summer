using System.Threading.Tasks;

namespace Summer.Infrastructure.SeedWork
{
    public interface IDataSeed
    {
        Task SeedAsync();
    }
}