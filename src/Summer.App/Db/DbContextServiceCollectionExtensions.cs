using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Summer.App.Db
{
    public static class DbContextServiceCollectionExtensions
    {
        public static IServiceCollection AddSummerDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<SummerDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}