using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Shb.Domain.Abstractions;
using Shb.Domain.Models;
using Shb.Infrastructure.Persistence.Database;
using Shb.Infrastructure.Persistence.Database.Repositories;
using Shb.Infrastructure.Persistence.Database.Repositories.Abstraction;
using Xunit;


namespace Shb.Infrastructure.UnitTests
{
    public class SuperheroRepositoryTests
    {
        private static readonly Fixture fixture = new Fixture();
        public SuperheroRepositoryTests()
        {
        }

        [Theory]
        [AutoMoqData]
        public async Task SingleByIdAsync_should_return_expected_superheroes(
            IEnumerable<Superhero> superheroes,
            IAppLogger<EfRepository<Superhero, int>> logger)
        {

            // Arrange
            var options = new DbContextOptionsBuilder<ShbContext>()
                                .UseInMemoryDatabase(databaseName: "ShbDb")
                                .Options;

            using (var dbContext = new ShbContext(options)) 
            {
                dbContext.AddRange(superheroes);
                dbContext.SaveChanges();
            }

            using (var dbContext = new ShbContext(options)) 
            {
                SuperheroRepository repository = new SuperheroRepository(dbContext, logger);   
                
                //Act
                Superhero result = await repository.SingleByIdAsync(superheroes.First().Id);

                //Assert 
                result.Should().NotBeNull();
                result.Id.Should().Be(superheroes.First().Id);
            }
        }    
    }
}