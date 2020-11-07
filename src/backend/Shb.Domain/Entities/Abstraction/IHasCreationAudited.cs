using System;

namespace Shb.Domain.Entities.Abstraction
{
    public interface IHasCreationAudited<TPrimaryKeyType> where TPrimaryKeyType : struct
    {
         DateTime CreatedAtTime { get; set; }
         TPrimaryKeyType CreatedByUserId { get; set; }
    }
}
