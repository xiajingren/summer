using MediatR;

namespace Summer.Application.Apis.Auth.UpdateCurrentUserProfile
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