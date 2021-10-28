using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;

namespace Summer.Infrastructure.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string Hash(User user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(user.SecurityStamp)) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            password = user.SecurityStamp + password;

            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            var salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public bool Verify(User user, string password)
        {
            var hashed = Hash(user, password);
            return hashed == user.PasswordHash;
        }
    }
}