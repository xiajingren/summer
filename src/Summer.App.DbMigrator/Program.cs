using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Summer.App.Db;
using Summer.App.DI;

namespace Summer.App.DbMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DbMigrateService>()
                        .AddSummerDbContext(hostContext.Configuration.GetConnectionString("Default"))
                        .AddSingleton<DbInitializer>();
                });
    }
}
