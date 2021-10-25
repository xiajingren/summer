using System;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;

namespace Summer.Infrastructure.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string Hash(User user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (password == null) throw new ArgumentNullException(nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(user.SecurityStamp + password);
        }

        public bool Verify(User user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (password == null) throw new ArgumentNullException(nameof(password));

            return BCrypt.Net.BCrypt.Verify(user.SecurityStamp + password, user.PasswordHash);
        }
    }
}