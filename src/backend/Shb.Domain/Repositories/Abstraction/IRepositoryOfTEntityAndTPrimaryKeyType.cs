using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shb.Domain.Entities.Abstraction;
using Shb.Domain.Specifications.Abstraction;

namespace Shb.Domain.Repositories.Abstraction
{
    public interface IRepository<TEntity, TPrimaryKeyType> 
        where TEntity : IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        Task<TEntity> SingleByIdAsync(TPrimaryKeyType id);
        Task<TEntity> SingleBySpecificationAsync(ISpecification<TEntity, TPrimaryKeyType> specification);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKeyType> specification);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> ExistsAsync(TPrimaryKeyType id);
        Task<int> CountAllAsync();
        Task<int> CountAsync(ISpecification<TEntity, TPrimaryKeyType> specification);
    }
}
