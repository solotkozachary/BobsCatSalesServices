using zs.bcs.BobsCatSalesServices.Domain.Enumerations;

namespace zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity
{
    /// <summary>
    /// Data representing an phone number.
    /// </summary>
    public class Phone : AbstractPersonalDataEntity
    {
        /// <summary>
        /// The type of the phone.
        /// </summary>
        public PhoneType PhoneType { get; set; }

        /// <summary>
        /// The country code of the phone number.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The area code of the phone number.
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// The phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Flag indicating a preferred point of contact.
        /// </summary>
        public bool IsPreferred { get; set; }

        /// <summary>
        /// Flag indicating consent to send sms messages.
        /// </summary>
        public bool HasSmsConsent { get; set; }

        /// <summary>
        /// Remove personally identifiable information.
        /// </summary>
        public override void RemovePiiData()
        {
            PhoneNumber = string.Empty;
        }

        /// <summary>
        /// EF relational entity.
        /// </summary>
        public SalesAssociate.SalesAssociate SalesAssociate { get; set; }
    }
}
