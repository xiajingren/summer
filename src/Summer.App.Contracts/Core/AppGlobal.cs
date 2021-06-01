using Microsoft.AspNetCore.Http;

namespace Summer.App.Contracts.Core
{
    public static class AppGlobal
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static HttpContext HttpContext => _httpContextAccessor.HttpContext;

        internal static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }

}