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
        private readonly IReadRepository<User> _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(IUserManager userManager, IReadRepository<User> userRepository,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetBySpecAsync(new UserByUserNameSpec(request.UserName),
                cancellationToken);
            if (user == null)
            {
                throw new BusinessException("用户名或密码错误");
            }

            var passed = _userManager.CheckPassword(user, request.Password);
            if (!passed)
            {
                throw new BusinessException("用户名或密码错误");
            }

            return await _jwtTokenService.IssueTokenAsync(user);
        }
    }
}