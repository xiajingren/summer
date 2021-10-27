using Summer.Domain.SeedWork;

namespace Summer.Domain.Events
{
    public class UserPasswordUpdatedEvent : BaseEvent
    {
        public int UserId { get; }

        public UserPasswordUpdatedEvent(int userId)
        {
            UserId = userId;
        }
    }
}