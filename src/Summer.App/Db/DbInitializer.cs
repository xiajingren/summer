using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.App.Business.Entities;

namespace Summer.App.Db
{
    public class DbInitializer
    {
        private readonly AppDbContext _appDbContext;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
        }

        public async Task Initialize()
        {
            await _appDbContext.Database.MigrateAsync();

            //todo: 初始化种子数据
            if (await _appDbContext.SysUsers.AnyAsync())
                return;

            var sysUsers = new SysUser[]
            {
                new SysUser() {UserName = "admin", Password = "123456", Name = "小黑"},
            };
            await _appDbContext.SysUsers.AddRangeAsync(sysUsers);

            await _appDbContext.SaveChangesAsync();
        }
    }
}