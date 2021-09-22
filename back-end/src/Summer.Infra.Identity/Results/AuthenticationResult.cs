using System.Text.Json.Serialization;

namespace Summer.Infra.Identity.Results
{
    public class AuthenticationResult
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")] public string TokenType { get; set; } = "Bearer";

        [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; }
    }
}