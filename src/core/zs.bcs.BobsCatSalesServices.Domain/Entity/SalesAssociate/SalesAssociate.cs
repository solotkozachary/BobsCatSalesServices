using System.Collections.Generic;
using zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity;

namespace zs.bcs.BobsCatSalesServices.Domain.Entity.SalesAssociate
{
    /// <summary>
    /// Represents an associate of Bob's Cat Sales.
    /// </summary>
    public class SalesAssociate : BobsCatSalesEntity
    {
        /// <summary>
        /// The person's name.
        /// </summary>
        public PersonalDesignation PersonalDesignation { get; set; }

        /// <summary>
        /// The person's addresses.
        /// </summary>
        public IEnumerable<Address> Addresses { get; set; }

        /// <summary>
        /// The person's phone numbers.
        /// </summary>
        public IEnumerable<Phone> PhoneNumbers { get; set; }
    }
}
