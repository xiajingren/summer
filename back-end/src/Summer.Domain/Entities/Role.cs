using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class Role : BaseEntity, IAggregateRoot
    {
        public string Name { get; internal set; }

        private Role()
        {
            // required by EF
        }

        internal Role(string name)
        {
            Name = name;
        }
    }
}