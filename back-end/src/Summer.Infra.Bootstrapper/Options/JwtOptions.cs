using System;

namespace Summer.Infra.Bootstrapper.Options
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}