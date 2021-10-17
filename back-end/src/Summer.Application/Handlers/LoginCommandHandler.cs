using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Infrastructure.Identity.Managers;

namespace Summer.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IIdentityManager _identityManager;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IIdentityManager identityManager, IMapper mapper)
        {
            _identityManager = identityManager;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await _identityManager.LoginAsync(request.UserName, request.Password);
            return _mapper.Map<TokenResponse>(token);
        }
    }
}