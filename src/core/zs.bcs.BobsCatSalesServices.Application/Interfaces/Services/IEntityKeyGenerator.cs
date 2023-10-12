using System.Threading.Tasks;
using System.Threading;

namespace zs.bcs.BobsCatSalesServices.Application.Interfaces.Services
{
    /// <summary>
    /// A contract for a service that generates entity keys.
    /// </summary>
    public interface IEntityKeyGenerator
    {
        /// <summary>
        /// Generate a unique identifier for an entity.
        /// </summary>
        /// <returns>A unique identifier for an entity.</returns>
        Task<string> GenerateKey(CancellationToken cancellationToken);
    }
}
