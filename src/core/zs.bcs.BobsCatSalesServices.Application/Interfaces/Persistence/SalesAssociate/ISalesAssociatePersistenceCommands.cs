using System.Threading;
using System.Threading.Tasks;

namespace zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate
{
    /// <summary>
    /// A contract for a service that performs entity commands.
    /// </summary>
    public interface ISalesAssociatePersistenceCommands
    {
        /// <summary>
        /// Create a sales associate.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        Task CreateSalesAssociate(Domain.Entity.SalesAssociate.SalesAssociate entity, CancellationToken cancellationToken);
    }
}
