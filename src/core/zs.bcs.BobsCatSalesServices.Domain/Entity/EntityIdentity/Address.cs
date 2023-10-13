using zs.bcs.BobsCatSalesServices.Domain.Enumerations;

namespace zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity
{
    /// <summary>
    /// Data representing an address.
    /// </summary>
    public class Address : AbstractPersonalDataEntity
    {
        /// <summary>
        /// The type of the address.
        /// </summary>
        public AddressType AddressType { get; set; }

        /// <summary>
        /// Address line one.
        /// </summary>
        public string AddressLineOne { get; set; }

        /// <summary>
        /// Address line two.
        /// </summary>
        public string AddressLineTwo { get; set; }

        /// <summary>
        /// The entity location's address unit.
        /// </summary>
        /// <example>Apt #3, Suite 200</example>
        public string Unit { get; set; }

        /// <summary>
        /// Address city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Address state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Address postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Flag indicating a preferred point of contact.
        /// </summary>
        public bool IsPreferred { get; set; }

        /// <summary>
        /// Flag indicating consent to send communications to this physical address.
        /// </summary>
        public bool HasMailerConsent { get; set; }

        /// <summary>
        /// Remove personally identifiable information.
        /// </summary>
        public override void RemovePiiData()
        {
            AddressLineOne = string.Empty;
            AddressLineTwo = string.Empty;
            Unit = string.Empty;
        }

        /// <summary>
        /// EF relational entity.
        /// </summary>
        public SalesAssociate.SalesAssociate SalesAssociate { get; set; }
    }
}
