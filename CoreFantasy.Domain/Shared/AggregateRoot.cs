namespace CoreFantasy.Domain.Shared
{
    public abstract class AggregateRoot
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;


        protected AggregateRoot()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow; 
        }
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
