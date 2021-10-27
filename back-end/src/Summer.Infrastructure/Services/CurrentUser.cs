using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Summer.Application.Interfaces;
using Summer.Domain.Entities;
using Summer.Domain.SeedWork;
using Summer.Infrastructure.Constants;

namespace Summer.Infrastructure.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReadRepository<User> _userRepository;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IReadRepository<User> userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
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

        public async Task<User> GetUserAsync()
        {
            if (!IsAuthenticated)
            {
                return null;
            }

            return await _userRepository.GetByIdAsync(Id);
        }
    }
}