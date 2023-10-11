namespace zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity
{
    /// <summary>
    /// Provides a way to remove personal identifiable information.
    /// </summary>
    public interface IPiiData
    {
        /// <summary>
        /// Remove personal identifiable information.
        /// </summary>
        void RemovePiiData();
    }
}
