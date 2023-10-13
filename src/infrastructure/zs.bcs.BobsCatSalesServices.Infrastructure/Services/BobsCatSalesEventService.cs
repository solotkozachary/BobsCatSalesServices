using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Domain.Entity;
using zs.bcs.BobsCatSalesServices.Events;

namespace zs.bcs.BobsCatSalesServices.Infrastructure.Services
{
    /// <summary>
    /// An implementation of a service that publishes events.
    /// </summary>
    public class BobsCatSalesEventService<T, U> : AbstractEventService<T, U>
        where U : BobsCatSalesEntity, new()
        where T : AbstractEvent<T, U>
    {
        private readonly ILogger<BobsCatSalesEventService<T, U>> _logger;

        public BobsCatSalesEventService(
            ILogger<BobsCatSalesEventService<T, U>> logger
            )
        {
            _logger = logger;
        }

        protected override Task PublishEvent(T applicationEvent, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter PublishEvent - EventId:{EventId}", applicationEvent.EventId);

            // Integrate with service bus here

            _logger.LogTrace("Exit PublishEvent - EventId:{EventId}", applicationEvent.EventId);

            return Task.CompletedTask;
        }
    }
}
