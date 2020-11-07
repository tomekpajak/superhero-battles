using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shb.Domain.Entities.Abstraction;
using Shb.Domain.Specifications.Abstraction;

namespace Shb.Infrastructure.Persistence.Database.Abstractions
{
    internal class SpecificationEvaluator<TEntity, TPrimaryKeyType> 
        where TEntity : BaseEntity<TPrimaryKeyType> 
        where TPrimaryKeyType : struct
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TPrimaryKeyType> specification) 
        {
            if (inputQuery is null)
            {
                throw new ArgumentNullException(nameof(inputQuery));
            }

            var query = inputQuery;

            if (specification is null)
            {
                return query;
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }

            if (specification.AsNoTracking) 
            {
                query = query.AsNoTracking();
            }

            return query;
        }
    }
}
