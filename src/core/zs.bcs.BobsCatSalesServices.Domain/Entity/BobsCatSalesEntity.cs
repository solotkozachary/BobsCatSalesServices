using System;

namespace zs.bcs.BobsCatSalesServices.Domain.Entity
{
    /// <summary>
    /// An entity in the Bob's Cat Sales domain.
    /// </summary>
    public class BobsCatSalesEntity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public string EntityKey { get; set; }

        /// <summary>
        /// The unique identifier of the entity this entity belongs to.
        /// </summary>
        public string RelationalEntityKey { get; set; }

        /// <summary>
        /// A flag to indicate if the entity record is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The date the entity was created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Who created the entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// The date the entity was updated on.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Who updated the entity.
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
