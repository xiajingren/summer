using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Summer.App.Contracts.Core
{
    public interface IAppStartup
    {
        IServiceCollection ConfigureServices(IServiceCollection services);

        async Task Start(IServiceProvider serviceProvider)
        {
            await Task.CompletedTask;
        }
    }
}