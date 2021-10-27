using System.Collections.Generic;

namespace Summer.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }

        private List<BaseEvent> _domainEvents;
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(BaseEvent eventItem)
        {
            _domainEvents ??= new List<BaseEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(BaseEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}