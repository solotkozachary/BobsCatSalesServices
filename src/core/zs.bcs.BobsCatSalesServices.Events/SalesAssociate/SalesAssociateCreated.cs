namespace zs.bcs.BobsCatSalesServices.Events.SalesAssociate
{
    /// <summary>
    /// The event published when a sales associate is created.
    /// </summary>
    public class SalesAssociateCreated : AbstractEvent<SalesAssociateCreated, Domain.Entity.SalesAssociate.SalesAssociate>
    {
        public SalesAssociateCreated(Domain.Entity.SalesAssociate.SalesAssociate entity) : base(entity) { }
    }
}
