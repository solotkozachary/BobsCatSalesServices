using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.Enumerations;
using zs.bcs.BobsCatSalesServices.Application.Exceptions;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Queries;

namespace zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving an associate user by id.
    /// </summary>
    public class GetSalesAssociateByEmailHandler : IRequestHandler<GetSalesAssociateByEmailQuery, Domain.Entity.SalesAssociate.SalesAssociate>
    {
        private readonly ISalesAssociatePersistenceQueries _queries;
        private readonly ILogger<GetSalesAssociateByEmailHandler> _logger;

        public GetSalesAssociateByEmailHandler(
            ISalesAssociatePersistenceQueries queries,
            ILogger<GetSalesAssociateByEmailHandler> logger
            )
        {
            _queries = queries;
            _logger = logger;
        }

        public async Task<Domain.Entity.SalesAssociate.SalesAssociate> Handle(GetSalesAssociateByEmailQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetSalesAssociateByEmailHandler");

            var entity = await _queries.GetSalesAssociateByEmail(request.EmailAddress, cancellationToken);

            if (request.MustExist && entity == null)
            {
                var errMsg = "No user exists for provided id";

                _logger.LogError(errMsg);

                _logger.LogTrace("Exit GetSalesAssociateByEmailHandler");

                throw new ApplicationException(ApplicationConcern.Entity, ApplicationRules.MustExist, errMsg);
            }

            _logger.LogTrace("Exit GetSalesAssociateByEmailHandler");

            return entity;
        }
    }
}
