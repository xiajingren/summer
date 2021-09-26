namespace Summer.Infrastructure.Identity.Dtos
{
    public class TokenOutput
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; } = "Bearer";

        public string RefreshToken { get; set; }
    }
}