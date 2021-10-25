using System;
using Microsoft.AspNetCore.Http;
using Summer.Application.Constants;
using Summer.Application.Interfaces;
using Summer.Domain.Interfaces;

namespace Summer.Application.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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
    }
}