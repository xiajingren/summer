using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.App.Entities;

namespace Summer.App.Db
{
    public class DbInitializer
    {
        private readonly SummerDbContext _summerDbContext;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _summerDbContext = serviceProvider.GetRequiredService<SummerDbContext>();
        }

        public async Task Initialize()
        {
            await _summerDbContext.Database.MigrateAsync();

            //todo: 初始化种子数据
            if (await _summerDbContext.SysUsers.AnyAsync())
                return;

            var sysUsers = new SysUser[]
            {
                new SysUser() {UserName = "admin", Password = "123456", Name = "小黑"},
            };
            await _summerDbContext.SysUsers.AddRangeAsync(sysUsers);

            await _summerDbContext.SaveChangesAsync();
        }
    }
}