using System.Text.Json.Serialization;

namespace Summer.Application.Responses
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")] public string TokenType { get; set; }

        [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; }

        public TokenResponse(string accessToken, int expiresIn, string tokenType, string refreshToken)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            TokenType = tokenType;
            RefreshToken = refreshToken;
        }
    }
}