namespace Summer.Application.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}