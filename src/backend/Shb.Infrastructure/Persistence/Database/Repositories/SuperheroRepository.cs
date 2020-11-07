using System;
using Microsoft.EntityFrameworkCore;
using Shb.Domain.Abstractions;
using Shb.Domain.Models;
using Shb.Domain.Repositories;
using Shb.Infrastructure.Persistence.Database.Repositories.Abstraction;

namespace Shb.Infrastructure.Persistence.Database.Repositories
{
    public class SuperheroRepository : EfRepository<Superhero, int>, ISuperheroRepository
    {
        private readonly DbSet<Superhero> superheroes;
        public SuperheroRepository(ShbContext dbContext, IAppLogger<EfRepository<Superhero, int>> logger) : base(dbContext, logger)
        {
            this.superheroes = dbContext.Set<Superhero>();
        }

    }
}
