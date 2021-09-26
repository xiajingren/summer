using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Infrastructure.Identity.Managers;

namespace Summer.Application.Requests.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenResponse>
    {
        private readonly IdentityManager _appIdentityManager;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IdentityManager appIdentityManager, IMapper mapper)
        {
            _appIdentityManager = appIdentityManager;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var token = await _appIdentityManager.RegisterAsync(request.UserName, request.Password);
            return _mapper.Map<TokenResponse>(token);
        }

    }
}