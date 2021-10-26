using System.Threading.Tasks;

namespace Summer.Domain.SeedWork
{
    public interface IDataSeed
    {
        Task SeedAsync();
    }
}