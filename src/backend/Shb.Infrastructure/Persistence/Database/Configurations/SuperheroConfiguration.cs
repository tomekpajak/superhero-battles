using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shb.Domain.Models;
using Shb.Infrastructure.Persistence.Database;

namespace Shb.Infrastructure.Persistence.Database.Configurations
{
    internal class SuperheroConfiguration : IEntityTypeConfiguration<Superhero>
    {
        public void Configure(EntityTypeBuilder<Superhero> builder)
        {
            builder.ToTable("Superheroes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(x => x.Attack)
                   .HasColumnName("Attack")
                   .IsRequired();     
            builder.Property(x => x.Defence)
                   .HasColumnName("Defence")
                   .IsRequired();                                   
        }
    }
}
