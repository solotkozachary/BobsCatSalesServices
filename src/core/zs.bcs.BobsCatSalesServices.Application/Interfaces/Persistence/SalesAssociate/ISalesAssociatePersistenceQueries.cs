using System.Threading;
using System.Threading.Tasks;

namespace zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate
{
    /// <summary>
    /// A contract for a service that performs entity queries.
    /// </summary>
    public interface ISalesAssociatePersistenceQueries
    {
        /// <summary>
        /// Retrieve an entity by email address.
        /// </summary>
        /// <param name="emailAddress">The email address to search by.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The entity found.</returns>
        Task<Domain.Entity.SalesAssociate.SalesAssociate> GetSalesAssociateByEmail(string emailAddress, CancellationToken cancellationToken);
    }
}
