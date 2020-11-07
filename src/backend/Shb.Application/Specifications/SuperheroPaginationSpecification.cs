using System;
using Shb.Domain.Models;
using Shb.Domain.Specifications.Abstraction;

namespace Shb.Application.Specifications
{
    internal class SuperheroPaginationSpecification : BaseSpecification<Superhero>
    {
        public SuperheroPaginationSpecification(int pageNumber, int pageSize)
        {
            this.ApplyPaging((pageNumber - 1) * pageSize, pageSize);
            this.ApplyAsNoTracking(true);
        }
    }
}
