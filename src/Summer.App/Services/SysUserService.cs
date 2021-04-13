using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Summer.App.Contracts.IServices;
using Summer.App.Db;

namespace Summer.App.Services
{
    public class SysUserService : BaseService, ISysUserService
    {
        private readonly ILogger<SysUserService> _logger;

        public SysUserService(ILogger<SysUserService> logger, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        public Task Hello()
        {
            _logger.LogInformation("Hello!!!");
            return Task.CompletedTask;
        }
    }
}