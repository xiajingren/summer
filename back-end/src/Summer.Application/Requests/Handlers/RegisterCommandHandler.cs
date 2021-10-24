using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Domain.Interfaces;

namespace Summer.Application.Requests.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenResponse>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public RegisterCommandHandler(IUserManager userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.CreateAsync(request.UserName, request.Password);
            return await _jwtTokenService.IssueTokenAsync(user);
        }
    }
}