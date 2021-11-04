using MediatR;

namespace Summer.Application.Apis.User.RegisterUser
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