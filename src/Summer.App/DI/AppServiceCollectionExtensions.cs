using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Summer.App.Db;
using Summer.App.Services;

namespace Summer.App.DI
{
    public static class AppServiceCollectionExtensions
    {
        public static IServiceCollection AddSummerDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<SummerDbContext>(options => options.UseSqlite(connectionString));
        }

        public static IServiceCollection AddSummerService(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly()
                ?.DefinedTypes.Where(p => p.IsAssignableTo(typeof(BaseService))).ToList();

            foreach (var type in types)
            {
                var topType = type.ImplementedInterfaces.FirstOrDefault(p => p.Name.EndsWith("Service"))?.GetTypeInfo();
                if (topType == null) continue;

                services.TryAddScoped(topType, type);
            }

            return services;
        }

    }
}