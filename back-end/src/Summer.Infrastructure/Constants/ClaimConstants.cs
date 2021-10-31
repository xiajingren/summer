using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Summer.Infrastructure.Constants
{
    public static class ClaimConstants
    {
        /// <summary>
        /// jwt id
        /// </summary>
        public const string Jti = JwtRegisteredClaimNames.Jti;

        /// <summary>
        /// expiration
        /// </summary>
        public const string Expiry = JwtRegisteredClaimNames.Exp;

        public const string UserId = JwtRegisteredClaimNames.Sub;

        public const string UserName = ClaimTypes.Name;

        public const string TenantId = "tenant";
    }
}