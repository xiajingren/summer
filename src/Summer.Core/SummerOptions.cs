﻿namespace Summer.Core
{
    public class SummerOptions
    {
        /// <summary>
        /// 生产环境禁用swagger
        /// </summary>
        public bool DisableSwaggerInProd { get; set; } = true;

        public string ConnectionString { get; set; }

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