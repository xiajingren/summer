using System;

namespace Summer.Infrastructure.Options
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}