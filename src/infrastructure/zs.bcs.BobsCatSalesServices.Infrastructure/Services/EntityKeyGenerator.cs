using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Services;

namespace zs.bcs.BobsCatSalesServices.Infrastructure.Services
{
    /// <summary>
    /// An implementation of a service that generates entity keys.
    /// </summary>
    public class EntityKeyGenerator : IEntityKeyGenerator
    {
        private readonly ILogger<EntityKeyGenerator> _logger;

        public EntityKeyGenerator(
            ILogger<EntityKeyGenerator> logger
            )
        {
            _logger = logger;
        }

        /// <summary>
        /// Generate a unique identifier for an entity.
        /// </summary>
        /// <returns>A unique identifier for an entity.</returns>
        public Task<string> GenerateKey(CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter EntityKeyGenerator");

            // We have more control over our entity key generation if we need it. For demo purposes, just using a GUID.

            var result = Guid.NewGuid().ToString();

            _logger.LogTrace("Exit EntityKeyGenerator - EntityKey;{EntityKey}", result);

            return Task.FromResult(result);
        }
    }
}
