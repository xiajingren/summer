using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    public class LoginCommand : IRequest<TokenResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        
        public LoginCommand(string password, string userName)
        {
            Password = password;
            UserName = userName;
        }
    }
}