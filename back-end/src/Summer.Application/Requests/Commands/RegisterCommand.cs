using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    public class RegisterCommand : IRequest<UserResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public RegisterCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}