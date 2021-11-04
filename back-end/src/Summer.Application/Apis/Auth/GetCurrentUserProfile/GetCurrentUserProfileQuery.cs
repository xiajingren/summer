using MediatR;

namespace Summer.Application.Apis.Auth.GetCurrentUserProfile
{
    public class GetCurrentUserProfileQuery : IRequest<CurrentUserProfileResponse>
    {
    }
}