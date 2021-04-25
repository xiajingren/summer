using Microsoft.Extensions.DependencyInjection;

namespace Summer.App.Contracts.Core
{
    public interface IAppStartup
    {
        IServiceCollection ConfigureServices(IServiceCollection services);
    }
}