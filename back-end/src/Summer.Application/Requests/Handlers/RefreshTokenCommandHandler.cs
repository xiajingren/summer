using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Requests.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IReadRepository<User> _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public RefreshTokenCommandHandler(IReadRepository<User> userRepository, IJwtTokenService jwtTokenService,
            IRepository<RefreshToken> refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var passed = _jwtTokenService.ValidateExpiredToken(request.AccessToken, out var jwtId);
            if (!passed)
            {
                throw new BusinessException();
            }

            var refreshToken =
                await _refreshTokenRepository.GetBySpecAsync(new RefreshTokenByTokenSpec(request.RefreshToken),
                    cancellationToken);

            if (refreshToken == null)
            {
                throw new BusinessException();
            }

            refreshToken.Confirm(jwtId);
            await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);

            var user = await _userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
            return await _jwtTokenService.IssueTokenAsync(user);
        }
    }
}