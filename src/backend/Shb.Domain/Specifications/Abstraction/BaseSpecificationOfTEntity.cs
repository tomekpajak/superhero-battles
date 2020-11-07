using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shb.Domain.Entities.Abstraction;

namespace Shb.Domain.Specifications.Abstraction
{
    public abstract class BaseSpecification<TEntity> : BaseSpecification<TEntity, int>, ISpecification<TEntity>
        where TEntity : IEntity<int>, IEntity
    {
    }
}


