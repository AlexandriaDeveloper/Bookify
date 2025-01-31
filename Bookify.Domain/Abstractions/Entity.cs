namespace Bookify.Domain.Abstractions
{
    public abstract class Entity : IEquatable<Entity>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        protected Entity()
        {
            
        }
        protected Entity(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; init; }

        public bool Equals(Entity? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id;


        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);

        }
    }
}
