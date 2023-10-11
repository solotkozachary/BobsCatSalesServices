using MediatR;

namespace zs.bcs.BobsCatSalesServices.Application.Entity.Commands
{
    /// <summary>
    /// A command to initialize an entity.
    /// </summary>
    public class InitializeEntityCommand<T> : IRequest<T>
        where T : Domain.Entity.BobsCatSalesEntity
    {
        /// <summary>
        /// The unique identifier of the entity the entity belongs to.
        /// </summary>
        public string RelationalEntityKey { get; set; }

        /// <summary>
        /// The unique identifier of the source that created the entity.
        /// </summary>
        public string EntityCreationSourceId { get; set; }
    }
}
