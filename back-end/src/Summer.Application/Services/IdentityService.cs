using System.Threading.Tasks;
using MediatR;
using Summer.Application.Requests;
using Summer.Application.Responses;
using Summer.Domain.Identity.Commands;
using Summer.Shared.Dtos;

namespace Summer.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IMediator _mediator;

        public IdentityService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<OutputDto<TokenResponse>> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OutputDto<TokenResponse>> Register(RegisterRequest input)
        {
            var result = await _mediator.Send(new RegisterCommand(input.UserName, input.Password));
            return new OutputDto<TokenResponse>(new TokenResponse());
        }

        public Task<OutputDto<TokenResponse>> RefreshToken(string token, string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}