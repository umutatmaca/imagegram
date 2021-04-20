using System;

namespace Imagegram.Core.Domain
{
    public interface IDomainEntity
    {
        Guid Id { get; set; }
    }
}
