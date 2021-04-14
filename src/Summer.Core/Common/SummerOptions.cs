using Microsoft.Extensions.DependencyInjection;

namespace Summer.Core.Common
{
    public class SummerOptions
    {
        /// <summary>
        /// 生产环境禁用swagger
        /// </summary>
        public bool DisableSwaggerInProd { get; set; } = true;
    }
}
