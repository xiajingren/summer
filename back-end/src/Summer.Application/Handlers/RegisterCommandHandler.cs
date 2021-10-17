using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Infrastructure.Identity.Managers;

namespace Summer.Application.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenResponse>
    {
        private readonly IIdentityManager _identityManager;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IIdentityManager identityManager, IMapper mapper)
        {
            _identityManager = identityManager;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var token = await _identityManager.RegisterAsync(request.UserName, request.Password);
            return _mapper.Map<TokenResponse>(token);
        }
    }
}