using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate;

namespace zs.bcs.BobsCatSalesServices.Persistence.MsSql.Services.SalesAssociate
{
    /// <summary>
    /// An implementation of a service that performs entity commands.
    /// </summary>
    public class SalesAssociatePersistenceCommands : ISalesAssociatePersistenceCommands
    {
        private readonly BobsCatSalesDbContext _context;
        private readonly ILogger<SalesAssociatePersistenceCommands> _logger;

        public SalesAssociatePersistenceCommands(
            BobsCatSalesDbContext context,
            ILogger<SalesAssociatePersistenceCommands> logger
            )
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateSalesAssociate(Domain.Entity.SalesAssociate.SalesAssociate entity, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateSalesAssociate");

            await _context.SalesAssociates.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync();

            _logger.LogTrace("Exit CreateSalesAssociate");
        }
    }
}
