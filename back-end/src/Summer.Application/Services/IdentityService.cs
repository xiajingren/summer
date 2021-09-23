using System.Threading.Tasks;
using MediatR;
using Summer.Application.Dtos;
using Summer.Domain.Identity.Commands;
using Summer.Shared.Dtos;

namespace Summer.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly Mediator _mediator;

        public IdentityService(Mediator mediator)
        {
            _mediator = mediator;
        }

        public Task<OutputDto<TokenOutputDto>> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OutputDto<TokenOutputDto>> Register(RegisterInputDto input)
        {
            var result = await _mediator.Send(new RegisterCommand(input.UserName, input.Password));
            return new OutputDto<TokenOutputDto>(new TokenOutputDto());
        }

        public Task<OutputDto<TokenOutputDto>> RefreshToken(string token, string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}