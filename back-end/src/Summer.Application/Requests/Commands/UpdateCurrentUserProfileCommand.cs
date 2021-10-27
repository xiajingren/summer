using MediatR;

namespace Summer.Application.Requests.Commands
{
    public class UpdateCurrentUserProfileCommand : IRequest
    {
        public string UserName { get; set; }

        public UpdateCurrentUserProfileCommand(string userName)
        {
            UserName = userName;
        }
    }
}