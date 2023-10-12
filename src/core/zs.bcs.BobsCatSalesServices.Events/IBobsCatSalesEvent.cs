using System;
using zs.bcs.BobsCatSalesServices.Domain.Entity;

namespace zs.bcs.BobsCatSalesServices.Events
{
    /// <summary>
    /// A contract to reinforce event model definition.
    /// </summary>
    /// <typeparam name="T">The type of the event.</typeparam>
    /// <typeparam name="U">The type of entity the event is for.</typeparam>
    public interface IBobsCatSalesEvent<T, U>
        where U : BobsCatSalesEntity, new()
        where T : IBobsCatSalesEvent<T, U>
    {
        /// <summary>
        /// The type of the event.
        /// </summary>
        public Type EventType { get { return typeof(T); } }

        /// <summary>
        /// The type of the event data.
        /// </summary>
        public Type EventDataType { get { return typeof(U); } }
    }
}
