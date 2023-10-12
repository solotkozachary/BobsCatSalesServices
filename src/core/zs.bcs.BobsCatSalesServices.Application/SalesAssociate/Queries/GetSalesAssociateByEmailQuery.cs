using MediatR;

namespace zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Queries
{
    /// <summary>
    /// The query to get a sales associate by email.
    /// </summary>
    public class GetSalesAssociateByEmailQuery : IRequest<Domain.Entity.SalesAssociate.SalesAssociate>
    {
        /// <summary>
        /// The email address to search by.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Flag to indicate entity must exist.
        /// </summary>
        public bool MustExist { get; set; }
    }
}
