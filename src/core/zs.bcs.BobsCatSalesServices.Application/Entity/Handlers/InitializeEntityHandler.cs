using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using zs.bcs.BobsCatSalesServices.Application.Entity.Commands;
using zs.bcs.BobsCatSalesServices.Application.Enumerations;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Services;

namespace zs.bcs.BobsCatSalesServices.Application.Entity.Handlers
{
    /// <summary>
    /// The handler responsible for initializing a new entity.
    /// </summary>
    public class InitializeEntityHandler<U, T> : IRequestHandler<InitializeEntityCommand<T>, T>
        where U : IRequest<T>
        where T : Domain.Entity.BobsCatSalesEntity
    {
        private readonly ILogger<InitializeEntityHandler<U, T>> _logger;
        private readonly IEntityKeyGenerator _keyGenerator;

        public InitializeEntityHandler(
            ILogger<InitializeEntityHandler<U, T>> logger,
            IEntityKeyGenerator keyGenerator
            )
        {
            _logger = logger;
            _keyGenerator = keyGenerator;
        }

        public async Task<T> Handle(InitializeEntityCommand<T> request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter InitializeEntityHandler - EntityType:{EntityType}", typeof(T));

            T result = default;

            try
            {
                var entity = (Domain.Entity.BobsCatSalesEntity)Activator.CreateInstance(typeof(T));

                var creationSourceId = !string.IsNullOrWhiteSpace(request.EntityCreationSourceId) ? request.EntityCreationSourceId : nameof(InitializeEntityHandler<U, T>);

                entity.EntityKey = await _keyGenerator.GenerateKey(cancellationToken);
                entity.RelationalEntityKey = request.RelationalEntityKey;
                entity.IsActive = true;
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = creationSourceId;
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = creationSourceId;

                result = (T)entity;
            }
            catch (Exception ex)
            {
                var errMsg = "Entity initialization for entity with type {EntityType} failed.";

                _logger.LogError(ex, errMsg, typeof(T));

                _logger.LogTrace("Exit InitializeEntityHandler - EntityType:{EntityType}", typeof(T));

                throw new Exceptions.ApplicationException(ApplicationConcern.Entity, ApplicationRules.MustInitialize, errMsg.Replace("{EntityType}", typeof(T).ToString()));
            }

            _logger.LogInformation("A new Entity of type {EntityType} and with EntityKey {EntityKey} has been successfully created.",
                typeof(T), result.EntityKey);

            _logger.LogTrace("Exit InitializeEntityHandler - EntityKey:{EntityKey}   EntityType:{EntityType}", result.EntityKey, typeof(T));

            return result;
        }
    }
}
