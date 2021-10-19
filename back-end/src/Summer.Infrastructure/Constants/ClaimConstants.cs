using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Summer.Infrastructure.Constants
{
    public static class ClaimConstants
    {
        /// <summary>
        /// jwt id
        /// </summary>
        public static string Jti = JwtRegisteredClaimNames.Jti;

        /// <summary>
        /// expiration
        /// </summary>
        public static string Expiry = JwtRegisteredClaimNames.Exp;


        public static string UserId = JwtRegisteredClaimNames.Sub;


        public static string UserName = ClaimTypes.Name;

    }
}