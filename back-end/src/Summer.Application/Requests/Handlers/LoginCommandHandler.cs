using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        public Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}