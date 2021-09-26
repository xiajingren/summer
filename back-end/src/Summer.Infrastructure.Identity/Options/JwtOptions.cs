using System;

namespace Summer.Infrastructure.Identity.Options
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}