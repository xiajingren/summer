using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Summer.App.Contracts.Base.IServices;
using Summer.App.Db;

namespace Summer.App.Base.Services
{
    internal class BaseService : IBaseService
    {
        internal IServiceProvider ServiceProvider { get; }

        internal AppDbContext AppDbContext => ServiceProvider.GetRequiredService<AppDbContext>();
        internal ILogger Logger => ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(GetType());
        internal IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

        public BaseService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
