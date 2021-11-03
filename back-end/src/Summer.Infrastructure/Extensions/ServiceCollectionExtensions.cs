using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Summer.Domain.SeedWork;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContextWithDefaultRepository<T>(this IServiceCollection services) where T : BaseDbContext
        {
            services.AddDbContext<T>().AddDefaultRepository<T>();
        }

        public static void AddDefaultRepository<T>(this IServiceCollection services) where T : BaseDbContext
        {
            var entityTypes = typeof(T).GetTypeInfo()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsGenericType &&
                            typeof(DbSet<>).IsAssignableFrom(x.PropertyType.GetGenericTypeDefinition()) &&
                            typeof(IAggregateRoot).IsAssignableFrom(x.PropertyType.GenericTypeArguments[0]))
                .Select(x => x.PropertyType.GenericTypeArguments[0]);

            foreach (var entityType in entityTypes)
            {
                var readRepositoryType = typeof(IReadRepository<>).MakeGenericType(entityType);
                var repositoryType = typeof(IRepository<>).MakeGenericType(entityType);
                var repositoryImpType = typeof(EfRepository<,>).MakeGenericType(entityType, typeof(T));
                services.AddTransient(repositoryType, repositoryImpType);
                services.AddTransient(readRepositoryType, repositoryImpType);
            }
        }
    }
}