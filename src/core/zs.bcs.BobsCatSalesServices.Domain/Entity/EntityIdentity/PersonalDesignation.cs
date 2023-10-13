namespace zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity
{
    /// <summary>
    /// A person's name.
    /// </summary>
    public class PersonalDesignation : AbstractPersonalDataEntity
    {
        /// <summary>
        /// The first name of the entity.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the entity.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The middle initial of the entity.
        /// </summary>
        public string MiddleInitial { get; set; }

        /// <summary>
        /// The prefix of the entity's name.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// The suffix of the entity's name.
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Remove personal identifiable information.
        /// </summary>
        public override void RemovePiiData()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            MiddleInitial = string.Empty;
            Prefix = string.Empty;
            Suffix = string.Empty;
        }
    }
}
