using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Summer.Infra.Identity.Models;
using Summer.Infra.Identity.Results;
using Summer.Shared.Dtos;

namespace Summer.Infra.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<OutputDto<AuthenticationResult>> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<OutputDto<AuthenticationResult>> Register(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<OutputDto<AuthenticationResult>> RefreshToken(string token, string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}