using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate;

namespace zs.bcs.BobsCatSalesServices.Persistence.MsSql.Services.SalesAssociate
{
    /// <summary>
    /// An implementation of a service that performs entity queries.
    /// </summary>
    public class SalesAssociatePersistenceQueries : ISalesAssociatePersistenceQueries
    {
        private readonly BobsCatSalesDbContext _context;
        private readonly ILogger<SalesAssociatePersistenceQueries> _logger;

        public SalesAssociatePersistenceQueries(
            BobsCatSalesDbContext context,
            ILogger<SalesAssociatePersistenceQueries> logger
            )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Domain.Entity.SalesAssociate.SalesAssociate> GetSalesAssociateByEmail(string emailAddress, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetSalesAssociateByEmail");

            var entity = _context.SalesAssociates
                .Include(x => x.PersonalDesignation)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.EmailAddresses)
                .Include(x => x.Addresses)
                .Where(x => x.EmailAddresses.Where(y => y.EmailAddress == emailAddress).Any()).FirstOrDefault()
                ;

            _logger.LogTrace("Exit GetSalesAssociateByEmail");

            return entity;
        }
    }
}
