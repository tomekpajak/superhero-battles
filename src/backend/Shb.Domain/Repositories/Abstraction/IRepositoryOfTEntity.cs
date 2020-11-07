using System;
using Shb.Domain.Entities.Abstraction;

namespace Shb.Domain.Repositories.Abstraction
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : IEntity<int>, IEntity
    {
    }
}
