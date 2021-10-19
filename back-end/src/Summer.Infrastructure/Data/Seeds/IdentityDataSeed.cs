using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Summer.Domain.Entities;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure.Data.Seeds
{
    public class IdentityDataSeed : IDataSeed
    {
        private readonly SummerDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public IdentityDataSeed(SummerDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!_context.Users.Any())
            {
                await _context.Users.AddRangeAsync(GetDefaultUsers());

                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<User> GetDefaultUsers()
        {
            var user = new User()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "123456");

            return new List<User>() { user };
        }

    }
}