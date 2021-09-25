using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    public class LoginCommand : IRequest<TokenResponse>
    {
    }
}