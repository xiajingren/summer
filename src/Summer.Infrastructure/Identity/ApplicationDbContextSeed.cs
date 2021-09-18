using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.Identity;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure.Identity
{
    public class ApplicationDbContextSeed : IDbContextSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();
        public ApplicationDbContextSeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(GetDefaultUser());

                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<ApplicationUser> GetDefaultUser()
        {
            var user =
                new ApplicationUser()
                {
                    Email = "demouser@microsoft.com",
                    Id = Guid.NewGuid().ToString(),
                    PhoneNumber = "1234567890",
                    UserName = "demouser@microsoft.com",
                    NormalizedEmail = "DEMOUSER@MICROSOFT.COM",
                    NormalizedUserName = "DEMOUSER@MICROSOFT.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };

            user.PasswordHash = _passwordHasher.HashPassword(user, "Pass@word1");

            return new List<ApplicationUser>()
            {
                user
            };
        }
    }
}