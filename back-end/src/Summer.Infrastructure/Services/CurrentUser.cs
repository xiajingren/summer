using Microsoft.AspNetCore.Http;
using Summer.Application.Interfaces;
using Summer.Infrastructure.Constants;

namespace Summer.Infrastructure.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public int Id
        {
            get
            {
                var v = _httpContextAccessor.HttpContext.User.FindFirst(ClaimConstants.UserId)?.Value;
                return v == null ? -1 : int.Parse(v);
            }
        }

        public string UserName => _httpContextAccessor.HttpContext.User.Identity.Name;

        public int TenantId
        {
            get
            {
                var v = _httpContextAccessor.HttpContext.User.FindFirst(ClaimConstants.TenantId)?.Value;
                return v == null ? -1 : int.Parse(v);
            }
        }
    }
}