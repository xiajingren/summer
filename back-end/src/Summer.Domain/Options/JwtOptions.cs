using System;

namespace Summer.Domain.Options
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}