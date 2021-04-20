using System.Collections.Generic;

namespace Summer.Core
{
    public class AppOptions
    {
        /// <summary>
        /// 生产环境禁用swagger
        /// </summary>
        public bool DisableSwaggerInProd { get; set; } = true;

        public Dictionary<string, string> ConnectionStrings { get; set; }

        public JwtOptions JwtOptions { get; set; }
    }

    public class JwtOptions
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireSeconds { get; set; }
    }
}