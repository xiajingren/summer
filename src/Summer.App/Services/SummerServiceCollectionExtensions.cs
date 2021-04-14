using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Summer.App.Services
{
    public static class SummerServiceCollectionExtensions
    {
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
