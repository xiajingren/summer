using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Summer.App.Db;
using System.Linq;
using System.Reflection;

namespace Summer.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSummerDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddSummerService(this IServiceCollection services)
        {
            //var types = Assembly.GetExecutingAssembly()
            //    ?.DefinedTypes.Where(p => p.IsAssignableTo(typeof(BaseService)) && !p.Name.StartsWith("Base")).ToList();
            var types = Assembly.GetExecutingAssembly()
                ?.DefinedTypes.Where(p => !p.Name.StartsWith("Base") && p.Name.EndsWith("Service")).ToList();

            foreach (var type in types)
            {
                var serviceType = type.ImplementedInterfaces.FirstOrDefault(p => p.Name == $"I{type.Name}")?.GetTypeInfo();
                if (serviceType == null) continue;

                services.TryAddScoped(serviceType, type);
            }

            return services;
        }

    }
}