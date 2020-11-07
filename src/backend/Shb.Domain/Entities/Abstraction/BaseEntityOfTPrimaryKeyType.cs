using System;

namespace Shb.Domain.Entities.Abstraction
{
    public abstract class BaseEntity<TPrimaryKeyType> : IEntity<TPrimaryKeyType> 
        where TPrimaryKeyType : struct
    {
        public virtual TPrimaryKeyType Id { get; set; }
    }
}
