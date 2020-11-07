using System;
using System.Linq.Expressions;
using Shb.Domain.Models;
using Shb.Domain.Specifications.Abstraction;

namespace Shb.Application.Specifications
{
    internal class SuperheroRandomSpecification : BaseSpecification<Superhero>
    {
        public SuperheroRandomSpecification(int superheroCount)
        {
            this.ApplyAsNoTracking(true);
            this.ApplyPaging(GetRandomSkip(superheroCount), 1);
        }

        private int GetRandomSkip(int count) 
        {
            var r = new Random();
            return (int)(r.NextDouble() * count);
        }
    }
}
