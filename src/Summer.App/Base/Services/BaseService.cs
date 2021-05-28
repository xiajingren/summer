using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Summer.App.Contracts.Base.Dtos;
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

        public virtual OutputDto<TDto> Ok<TDto>(TDto dto, string message = "操作成功") where TDto : class
        {
            return OutputDto<TDto>.CreateOkInstance(dto, message);
        }

        public virtual OutputDto<TDto> Fail<TDto>(TDto dto, string message = "操作失败") where TDto : class
        {
            return OutputDto<TDto>.CreateFailInstance(dto, message);
        }
    }
}