using System.Threading.Tasks;
using System.Threading;
using zs.bcs.BobsCatSalesServices.Domain.Entity;

namespace zs.bcs.BobsCatSalesServices.Events
{
    /// <summary>
    /// Contract for a service that publishes events.
    /// </summary>
    /// <remarks>Please use abstract service implementation to ensure PII protection -> <see cref="AbstractEventService{T, U}"/></remarks>
    public interface IBobsCatSalesEventService<T, U>
        where U : BobsCatSalesEntity, new()
        where T : IBobsCatSalesEvent<T, U>
    {
        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <param name="applicationEvent">The event to publish.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        Task PublishBobsCatSalesEvent(T applicationEvent, CancellationToken cancellationToken);
    }
}
