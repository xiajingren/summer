using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    public class RefreshTokenCommand : IRequest<TokenResponse>
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }


        public RefreshTokenCommand(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}