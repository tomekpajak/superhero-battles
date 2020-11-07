using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shb.Domain.Models;
using Shb.Infrastructure.Persistence.Database.Extensions;

namespace Shb.Infrastructure.Persistence.Database
{
    public class ShbContext : DbContext
    {        
        public ShbContext()
        {
        }
        
        public ShbContext(DbContextOptions<ShbContext> options) 
            : base(options)
        {
        }
        public DbSet<Superhero> Superheroes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Shb");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(builder);

            builder.SeedSuperheroes();
        }        
    }
}
