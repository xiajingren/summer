using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileResponse>
    {
    }
}