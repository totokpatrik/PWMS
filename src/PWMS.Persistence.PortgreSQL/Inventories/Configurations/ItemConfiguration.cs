using PWMS.Domain.Configuration.Inventory.Items.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Persistence.PortgreSQL.Inventories.Configurations;

public class ItemConfiguration : AuditableWarehouseConfiguration<Item>, IEntityTypeConfiguration<Item>
{
    public override void Configure(EntityTypeBuilder<Item> builder)
    {
        base.Configure(builder);

        builder.ToTable("Items");

        builder.HasKey(i => i.Id);

        builder
            .HasOne(i => i.ItemFamily)
            .WithMany(ifa => ifa.Items);

        builder
            .HasMany(i => i.Footprints)
            .WithOne(fp => fp.Item);

        builder
            .HasMany(i => i.AlternateItems)
            .WithOne(ai => ai.Item);
    }
}
