using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Requests.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IUserManager _userManager;
        private readonly IRepository<User> _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(IUserManager userManager, IRepository<User> userRepository,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetBySpecAsync(new UserByUserNameSpec(request.UserName),
                cancellationToken);
            if (user == null)
            {
                throw new BusinessException("用户名或密码错误");
            }

            var passed = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passed)
            {
                throw new BusinessException("用户名或密码错误");
            }

            return await _jwtTokenService.IssueTokenAsync(user);
        }
    }
}