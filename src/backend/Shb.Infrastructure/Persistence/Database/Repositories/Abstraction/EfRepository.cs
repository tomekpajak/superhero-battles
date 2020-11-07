using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shb.Domain.Abstractions;
using Shb.Domain.Entities.Abstraction;
using Shb.Domain.Repositories.Abstraction;
using Shb.Domain.Specifications.Abstraction;
using Shb.Infrastructure.Persistence.Database.Abstractions;

namespace Shb.Infrastructure.Persistence.Database.Repositories.Abstraction
{
    public class EfRepository<TEntity, TPrimaryKeyType> : IRepository<TEntity, TPrimaryKeyType>
        where TEntity : BaseEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        protected readonly ShbContext dbContext;
        protected readonly IAppLogger<EfRepository<TEntity, TPrimaryKeyType>> logger;

        public EfRepository(ShbContext dbContext, IAppLogger<EfRepository<TEntity, TPrimaryKeyType>> logger) 
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                await dbContext.Set<TEntity>().AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return entity;                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not add entity {Entity}", nameof(entity));
                throw;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {       
            try
            {
                dbContext.Remove<TEntity>(entity);                
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not delete entity {Entity}", nameof(entity));
                throw;
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                dbContext.Set<TEntity>().Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not update entity {Entity}", nameof(entity));
                throw;
            }
        }

        public async Task<TEntity> SingleByIdAsync(TPrimaryKeyType id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> SingleBySpecificationAsync(ISpecification<TEntity, TPrimaryKeyType> specification) 
        {
            try
            {
                return await ApplySpecification(specification).SingleAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not get single of entity {Entity}", nameof(TEntity));
                throw;
            }
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            return await this.ListAsync(null);
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TPrimaryKeyType> specification)
        {
            try
            {
                return await ApplySpecification(specification).ToListAsync();    
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not get list of entities {Entity}", nameof(TEntity));
                throw;
            }            
        }
        public async Task<bool> ExistsAsync(TPrimaryKeyType id)
        {
            return await dbContext.Set<TEntity>().AnyAsync(e => e.Id.Equals(id));
        }
        public async Task<int> CountAllAsync() 
        {
            return await this.CountAsync(null);
        }
        public async Task<int> CountAsync(ISpecification<TEntity, TPrimaryKeyType> specification) 
        {
            try
            {
                return await ApplySpecification(specification).CountAsync();   
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Could not get count of entities {nameof(TEntity)}");
                throw;
            }            
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TPrimaryKeyType> specification) => 
            SpecificationEvaluator<TEntity, TPrimaryKeyType>.GetQuery(dbContext.Set<TEntity>().AsQueryable(), specification);

        private bool ExistsInChangeTracker(TEntity entity) => 
            dbContext.Set<TEntity>().Any(e => e.Id.Equals(entity.Id));
    }
}
