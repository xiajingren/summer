using MediatR;

namespace Summer.Application.Apis.Auth.Login
{
    public class LoginCommand : IRequest<TokenResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public LoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}