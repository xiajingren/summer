using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Infrastructure.Identity.Managers;

namespace Summer.Application.Requests.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IIdentityManager _identityManager;
        private readonly IMapper _mapper;

        public RefreshTokenCommandHandler(IIdentityManager identityManager, IMapper mapper)
        {
            _identityManager = identityManager;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _identityManager.RefreshTokenAsync(request.AccessToken, request.RefreshToken);
            return _mapper.Map<TokenResponse>(token);
        }
    }
}