using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Core.Domain
{
    public interface IUnitOfWork
    {
        Task CommitAsync(DomainEntity domainEntity, CancellationToken cancellation);
    }
}
