using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Summer.Infrastructure.Identity.Entities;
using Summer.Shared.SeedWork;

namespace Summer.Infrastructure.Identity
{
    public class UserDbContextSeed : IDbContextSeed
    {
        private readonly UserDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserDbContextSeed(UserDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (!_context.Users.Any())
            {
                await _context.Users.AddRangeAsync(GetDefaultUser());

                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<User> GetDefaultUser()
        {
            var user = new User()
            {
                Email = "demouser@microsoft.com",
                PhoneNumber = "1234567890",
                UserName = "demouser@microsoft.com",
                NormalizedEmail = "DEMOUSER@MICROSOFT.COM",
                NormalizedUserName = "DEMOUSER@MICROSOFT.COM",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "Pass@word1");

            return new List<User>() { user };
        }

    }
}