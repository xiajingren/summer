using MediatR;
using Summer.Domain.Identity.Entities;

namespace Summer.Domain.Identity.Commands
{
    public class RegisterCommand : IRequest<ApplicationUser>
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