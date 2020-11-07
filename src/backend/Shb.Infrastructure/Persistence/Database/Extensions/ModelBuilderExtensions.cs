using System;
using Microsoft.EntityFrameworkCore;
using Shb.Domain.Models;

namespace Shb.Infrastructure.Persistence.Database.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static void SeedSuperheroes(this ModelBuilder modelBuilder) 
        {
            modelBuilder
                .Entity<Superhero>()
                .HasData(ShbContextSeed.GetSampleOfSuperheroes());
        }
    }
}
