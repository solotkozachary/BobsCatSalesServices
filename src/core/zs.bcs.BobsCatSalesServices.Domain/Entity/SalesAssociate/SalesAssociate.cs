using System;
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

        /// <summary>
        /// The person's email addresses.
        /// </summary>
        public IEnumerable<Email> EmailAddresses { get; set; }

        /// <summary>
        /// The last time the sales associate successfully logged into the system.
        /// </summary>
        public DateTime LastSuccessfulLogin { get; set; }

        /// <summary>
        /// The last time the sales associate unsuccessfully logged into the system.
        /// </summary>
        public DateTime? LastUnsuccessfulLogin { get; set; }

        /// <summary>
        /// The sales associate's password hash.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Password salt value.
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// A flag to indicate the sales associate needs to change their password.
        /// </summary>
        public bool NeedsToChangePassword { get; set; }

        /// <summary>
        /// The date the sales associate's current password was created.
        /// </summary>
        public DateTime PasswordCreatedDate { get; set; }

        /// <summary>
        /// A collection of hashes for passwords the sales associate has already used.
        /// </summary>
        public IEnumerable<byte[]> UsedPasswordHashes { get; set; }
    }
}
