using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Summer.App.Db;
using System.Linq;
using System.Reflection;
using Summer.App.Contracts.Core;


namespace Summer.App.Core
{
    public class AppStartup : IAppStartup
    {
        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var appOptions = services.BuildServiceProvider().GetRequiredService<AppOptions>();

            // EFCore
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(appOptions.ConnectionStrings["Default"]));

            // Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // AppServices
            var types = Assembly.GetExecutingAssembly()
                ?.DefinedTypes.Where(p => !p.Name.StartsWith("Base") && p.Name.EndsWith("Service")).ToArray();

            foreach (var type in types)
            {
                var serviceType = type.ImplementedInterfaces.FirstOrDefault(p => p.Name == $"I{type.Name}")
                    ?.GetTypeInfo();
                if (serviceType == null) continue;

                services.TryAddScoped(serviceType, type);
            }

            return services;
        }
    }
}