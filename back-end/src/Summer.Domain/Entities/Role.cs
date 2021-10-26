using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class Role : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string NormalizedName { get; private set; }

        private Role()
        {
            // required by EF
        }

        internal Role(string name)
        {
            SetName(name);
        }

        internal void SetName(string name)
        {
            Name = name;
            NormalizedName = Name.ToUpperInvariant();
        }
    }
}