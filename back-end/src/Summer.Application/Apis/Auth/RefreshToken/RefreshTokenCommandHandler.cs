using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Interfaces;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IReadRepository<Domain.Entities.User> _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRepository<Domain.Entities.RefreshToken> _refreshTokenRepository;

        public RefreshTokenCommandHandler(IReadRepository<Domain.Entities.User> userRepository, IJwtTokenService jwtTokenService,
            IRepository<Domain.Entities.RefreshToken> refreshTokenRepository)
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