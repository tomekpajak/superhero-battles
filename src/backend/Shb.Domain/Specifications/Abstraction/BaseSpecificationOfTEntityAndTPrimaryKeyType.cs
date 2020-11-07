using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shb.Domain.Entities.Abstraction;

namespace Shb.Domain.Specifications.Abstraction
{
    public abstract class BaseSpecification<TEntity, TPrimaryKeyType> : ISpecification<TEntity, TPrimaryKeyType> 
        where TEntity : IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        public bool AsNoTracking { get; private set; } = false;

        protected virtual void ApplyCriteria(Expression<Func<TEntity, bool>> criteria) => this.Criteria = criteria;
        protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression) => this.Includes.Add(includeExpression);
        protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression) => 
            this.OrderBy = orderByExpression;
        protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) => 
            this.OrderByDescending = orderByDescendingExpression;
        protected virtual void ApplyPaging(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
            this.IsPagingEnabled = true;
        }
        protected virtual void ApplyAsNoTracking(bool asNoTracking) => 
            this.AsNoTracking = asNoTracking;
    }
}
