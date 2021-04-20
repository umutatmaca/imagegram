using Imagegram.Core.Domain;
using Imagegram.Infrastructure.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMediator mediator;
        private readonly ImagegramContext imagegramContext;

        public UnitOfWork(IMediator mediator, ImagegramContext imagegramContext)
        {
            this.mediator = mediator;
            this.imagegramContext = imagegramContext;
        }
        public async Task CommitAsync(DomainEntity domainEntity, CancellationToken cancellation)
        {
            await DispatchEventsAsync(domainEntity, cancellation);

            await this.imagegramContext.SaveChangesAsync(cancellation);
        }

        private async Task DispatchEventsAsync(DomainEntity domainEntity, CancellationToken cancellationToken)
        {
            if (domainEntity.DomainEvents != null)
            {
                await Task.WhenAll(domainEntity.DomainEvents.Select(x => mediator.Publish(x, cancellationToken)));
            }
            domainEntity.ClearDomainEvents();
        }
    }
}
