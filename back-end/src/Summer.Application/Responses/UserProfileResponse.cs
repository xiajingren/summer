using MediatR;

namespace Summer.Application.Responses
{
    public class UserProfileResponse
    {
        public bool IsAuthenticated { get; set; }

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}