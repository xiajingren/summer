using MediatR;

namespace Summer.Application.Apis.Users.RegisterUser
{
    public class RegisterUserCommand : IRequest<UserResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public RegisterUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}