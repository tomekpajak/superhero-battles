using System;
using Shb.Domain.Entities.Abstraction;

namespace Shb.Domain.Models
{
    public class Superhero : BaseEntity
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
    }
}
