using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shb.Domain.Entities.Abstraction;

namespace Shb.Domain.Specifications.Abstraction
{
    public interface ISpecification<TEntity, TPrimaryKeyType> 
        where TEntity : IEntity<TPrimaryKeyType> 
        where TPrimaryKeyType : struct
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        bool AsNoTracking { get; }
    }
}