using System;
using Microsoft.AspNetCore.Http;
using Summer.Domain.Interfaces;
using Summer.Infrastructure.Constants;

namespace Summer.Infrastructure.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Id => _httpContextAccessor.HttpContext.User.FindFirst(ClaimConstants.UserId)?.Value;

        public string UserName => _httpContextAccessor.HttpContext.User.Identity.Name;
    }
}