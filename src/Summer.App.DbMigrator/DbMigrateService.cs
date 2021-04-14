using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Summer.App.Db;
using System.Threading;
using System.Threading.Tasks;

namespace Summer.App.DbMigrator
{
    public class DbMigrateService : IHostedService
    {
        private readonly ILogger<DbMigrateService> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly DbInitializer _dbInitializer;

        public DbMigrateService(ILogger<DbMigrateService> logger, IHostApplicationLifetime hostApplicationLifetime, DbInitializer dbInitializer)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
            _dbInitializer = dbInitializer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _dbInitializer.Initialize();

            _hostApplicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
