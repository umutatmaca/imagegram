using MediatR;
using System;

namespace Imagegram.Core.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
    }
}
