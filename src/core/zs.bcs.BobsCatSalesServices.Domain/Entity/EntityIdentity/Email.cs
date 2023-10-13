using zs.bcs.BobsCatSalesServices.Domain.Enumerations;

namespace zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity
{
    /// <summary>
    /// Data that represents an email
    /// </summary>
    public class Email : AbstractPersonalDataEntity
    {
        /// <summary>
        /// The type of the email.
        /// </summary>
        public EmailType EmailType { get; set; }

        /// <summary>
        /// The email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Flag indicating a preferred point of contact.
        /// </summary>
        public bool IsPreferred { get; set; }

        /// <summary>
        /// Flag indicating consent to send emails to this address.
        /// </summary>
        public bool HasEmailConsent { get; set; }

        /// <summary>
        /// Remove personally identifiable information.
        /// </summary>
        public override void RemovePiiData()
        {
            EmailAddress = string.Empty;
        }

        /// <summary>
        /// EF relational entity.
        /// </summary>
        public virtual SalesAssociate.SalesAssociate SalesAssociate { get; set; }
    }
}
