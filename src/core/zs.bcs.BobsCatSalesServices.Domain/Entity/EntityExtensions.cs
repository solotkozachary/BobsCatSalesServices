using System;
using System.Collections.Generic;
using System.Linq;
using zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity;

namespace zs.bcs.BobsCatSalesServices.Domain.Entity
{
    /// <summary>
    /// Extended functionality for Bob's Cat Sales entities.
    /// </summary>
    public static class EntityExtensions
    {
        private static readonly Type _piiType = typeof(IPiiData);

        /// <summary>
        /// Removes all PII from the entity.
        /// </summary>
        /// <param name="entity"></param>
        public static void RemovePersonalIdentifiableInformation(this BobsCatSalesEntity entity, bool updateRecord = false, string removalReferenceId = "RemovePiiProcess")
        {
            RemovePiiFromEntity(entity, new List<string>(), updateRecord, removalReferenceId);
        }

        private static void RemovePiiFromEntity(object obj, List<string> entityKeys, bool updateRecord = false, string removalReferenceId = "RemovePiiProcess")
        {
            if (obj != null)
            {
                if (obj is BobsCatSalesEntity entity)
                {
                    if (!entityKeys.Contains(entity.EntityKey))
                    {
                        entityKeys.Add(entity.EntityKey);
                    }
                    else
                    {
                        return;
                    }
                }

                var properties = obj.GetType().GetProperties();

                foreach (var property in properties)
                {
                    // Check nested properties.
                    if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && property.PropertyType != typeof(byte[]))
                    {
                        RemovePiiFromEntity(property.GetValue(obj), entityKeys);
                    }

                    // Remove PII from this entity.
                    if (_piiType.IsAssignableFrom(property.PropertyType))
                    {
                        var propertyValue = property.GetValue(obj);

                        if (propertyValue is IPiiData piiEntity)
                        {
                            piiEntity.RemovePiiData();
                        }

                        // If we need to persist the PII removal.
                        if (updateRecord && propertyValue is AbstractPersonalDataEntity piiRecord)
                        {
                            piiRecord.IsIdentificationRemoved = true;
                            piiRecord.IdentificationRemovedOn = DateTime.Now;
                            piiRecord.IdentificationRemovalReference = removalReferenceId;
                        }
                    }
                }

            }
        }
    }
}
