using System;

namespace Shb.Domain.Entities.Abstraction
{
    public interface IHasModificationAudited<TPrimaryKeyType> where TPrimaryKeyType : struct
    {
        DateTime? LastModificatedAtTime { get; set; }
        TPrimaryKeyType? LastModificatedByUserId { get; set; }
    }
}
