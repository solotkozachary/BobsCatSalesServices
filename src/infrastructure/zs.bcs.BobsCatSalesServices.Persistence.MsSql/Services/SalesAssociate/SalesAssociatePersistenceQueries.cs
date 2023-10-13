using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate;

namespace zs.bcs.BobsCatSalesServices.Persistence.MsSql.Services.SalesAssociate
{
    public class SalesAssociatePersistenceQueries : ISalesAssociatePersistenceQueries
    {
        private readonly BobsCatSalesDbContext _context;

        public SalesAssociatePersistenceQueries(
            BobsCatSalesDbContext context
            )
        {
            _context = context;
        }

        public Task<Domain.Entity.SalesAssociate.SalesAssociate> GetSalesAssociateByEmail(string emailAddress, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
