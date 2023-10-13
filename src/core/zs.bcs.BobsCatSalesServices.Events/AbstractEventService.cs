using System.Threading.Tasks;
using System.Threading;
using zs.bcs.BobsCatSalesServices.Domain.Entity;

namespace zs.bcs.BobsCatSalesServices.Events
{
    /// <summary>
    /// An abstract implementation of an event service that handles removing PII.
    /// </summary>
    public abstract class AbstractEventService<T, U> : IBobsCatSalesEventService<T, U>
        where U : BobsCatSalesEntity, new()
        where T : AbstractEvent<T, U>
    {
        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <param name="applicationEvent">The event to publish.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        public async Task PublishBobsCatSalesEvent(T applicationEvent, CancellationToken cancellationToken)
        {
            var eventData = applicationEvent.EventPayload;

            // Cyclic entity reference configured to build out EF DBContext currently causing issues.
            //eventData.RemovePersonalIdentifiableInformation();

            await PublishEvent(applicationEvent, cancellationToken);
        }

        protected abstract Task PublishEvent(T applicationEvent, CancellationToken cancellationToken);
    }
}
