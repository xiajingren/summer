using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Summer.Infra.Identity.Models;
using Summer.Infra.Identity.Dtos;
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

        public Task<OutputDto<TokenOutputDto>> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OutputDto<TokenOutputDto>> Register(RegisterInputDto input)
        {
            var existingUser = await _userManager.FindByNameAsync(input.UserName);
            if (existingUser != null)
            {
                return new OutputDto<TokenOutputDto>(new List<string>() { "username already exists" });
            }

            var newUser = new ApplicationUser() { UserName = input.UserName };
            var isCreated = await _userManager.CreateAsync(newUser, input.Password);
            if (!isCreated.Succeeded)
            {
                return new OutputDto<TokenOutputDto>(isCreated.Errors.Select(p => p.Description));
            }

            return new OutputDto<TokenOutputDto>(new TokenOutputDto());
        }

        public Task<OutputDto<TokenOutputDto>> RefreshToken(string token, string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}