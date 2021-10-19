using System.Text.Json.Serialization;
using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    public class RefreshTokenCommand : IRequest<TokenResponse>
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; }

        public RefreshTokenCommand(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}