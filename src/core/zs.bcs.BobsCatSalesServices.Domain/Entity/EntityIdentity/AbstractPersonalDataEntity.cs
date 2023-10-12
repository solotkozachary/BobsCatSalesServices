using System;

namespace zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity
{
    /// <summary>
    /// Abstract definition for a Bob's Cat Sales entity that contains PII.
    /// </summary>
    public abstract class AbstractPersonalDataEntity : BobsCatSalesEntity, IPiiData
    {
        /// <summary>
        /// A flag indicating that the data for this record has been removed from the system per request.
        /// </summary>
        public bool IsIdentificationRemoved { get; set; }

        /// <summary>
        /// The date the entity identification information was removed.
        /// </summary>
        public DateTime? IdentificationRemovedOn { get; set; }

        /// <summary>
        /// A reference Id used to link the removal of identifying information to a specific removal request.
        /// </summary>
        public string IdentificationRemovalReference { get; set; }

        /// <summary>
        /// Remove personal identifiable information.
        /// </summary>
        public abstract void RemovePiiData();
    }
}
