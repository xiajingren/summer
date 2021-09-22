using System;

namespace Summer.Infra.Bootstrapper.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}