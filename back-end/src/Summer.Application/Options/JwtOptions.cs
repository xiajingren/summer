using System;

namespace Summer.Application.Options
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}