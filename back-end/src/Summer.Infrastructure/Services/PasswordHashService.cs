using Ardalis.GuardClauses;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;

namespace Summer.Infrastructure.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string Hash(User user, string password)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrEmpty(password, nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(user.SecurityStamp + password);
        }

        public bool Verify(User user, string password)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrEmpty(password, nameof(password));

            return BCrypt.Net.BCrypt.Verify(user.SecurityStamp + password, user.PasswordHash);
        }
    }
}