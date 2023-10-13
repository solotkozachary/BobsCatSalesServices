using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Text.Json;
using System.Collections.Generic;
using zs.bcs.BobsCatSalesServices.Domain.Entity.SalesAssociate;

namespace zs.bcs.BobsCatSalesServices.Persistence.MsSql.EntityConfigurations
{
    /// <summary>
    /// Entity Framework persistence configurations on the Sales Associate entity.
    /// </summary>
    public class SalesAssociateConfiguration : IEntityTypeConfiguration<Domain.Entity.SalesAssociate.SalesAssociate>
    {
        public void Configure(EntityTypeBuilder<SalesAssociate> builder)
        {
            builder.Property(e => e.UsedPasswordHashes)
                .HasConversion(
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<IDictionary<string, DateTime>>(v, null as JsonSerializerOptions)
                );
        }
    }
}
