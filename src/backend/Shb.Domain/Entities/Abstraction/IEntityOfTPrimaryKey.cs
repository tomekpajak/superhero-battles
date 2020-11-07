using System;

namespace Shb.Domain.Entities.Abstraction
{
    public interface IEntity<TPrimaryKeyType> where TPrimaryKeyType : struct
    {
        TPrimaryKeyType Id { get; set; }
    }
}
