using System;
using Shb.Domain.Entities.Abstraction;

namespace Shb.Domain.Specifications.Abstraction
{
    public interface ISpecification<TEntity> : ISpecification<TEntity, int> 
        where TEntity : IEntity<int>, IEntity
    {
    }
}
