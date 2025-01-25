using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions
{
    public abstract class Entity :IEquatable<Entity>
    {
        protected Entity(Guid id)
        {
            Id = id;
        }
        public Guid Id { get;init ; }

        public bool Equals(Entity? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id;
  

        }
    }
}
