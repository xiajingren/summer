using Microsoft.Extensions.DependencyInjection;
using Summer.App.Db;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Summer.App.Contracts.IServices;

namespace Summer.App.Services
{
    internal class BaseService : IBaseService
    {
        internal IServiceProvider ServiceProvider { get; }

        internal SummerDbContext SummerDbContext => ServiceProvider.GetRequiredService<SummerDbContext>();
        internal ILogger Logger => ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(GetType());
        internal IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

        public BaseService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
