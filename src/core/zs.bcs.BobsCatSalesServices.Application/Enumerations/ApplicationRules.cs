namespace zs.bcs.BobsCatSalesServices.Application.Enumerations
{
    /// <summary>
    /// Values that express types of application rules.
    /// </summary>
    public enum ApplicationRules
    {
        /// <summary>
        /// Invalid default value.
        /// </summary>
        Default,

        /// <summary>
        /// Rule that indicates that something must exist.
        /// </summary>
        MustExist,

        /// <summary>
        /// Rule that indicates that something must not exist.
        /// </summary>
        MustNotExist,

        /// <summary>
        /// A rule that indicates that something must be valid.
        /// </summary>
        MustBeValid,

        /// <summary>
        /// Rule that indicates an entity must initialize.
        /// </summary>
        MustInitialize
    }
}
