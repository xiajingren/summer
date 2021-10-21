namespace Summer.Application.Responses
{
    public class CurrentUserProfileResponse
    {
        public bool IsAuthenticated { get; set; }

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}