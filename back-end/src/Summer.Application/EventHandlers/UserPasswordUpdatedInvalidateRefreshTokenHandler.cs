using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Domain.Entities;
using Summer.Domain.Events;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.EventHandlers
{
    public class UserPasswordUpdatedInvalidateRefreshTokenHandler : INotificationHandler<UserPasswordUpdatedEvent>
    {
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public UserPasswordUpdatedInvalidateRefreshTokenHandler(IRepository<RefreshToken> refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(UserPasswordUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var refreshTokens =
                await _refreshTokenRepository.ListAsync(new RefreshTokenByUserIdSpec(notification.UserId, true),
                    cancellationToken);

            foreach (var refreshToken in refreshTokens)
            {
                refreshToken.Invalidate();
                await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);
            }
        }
    }
}