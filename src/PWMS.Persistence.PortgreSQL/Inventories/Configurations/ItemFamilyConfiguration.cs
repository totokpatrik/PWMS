using PWMS.Domain.Configuration.Inventory.Items.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Persistence.PortgreSQL.Inventories.Configurations;

public class ItemFamilyConfiguration : AuditableWarehouseConfiguration<ItemFamily>, IEntityTypeConfiguration<ItemFamily>
{
    public override void Configure(EntityTypeBuilder<ItemFamily> builder)
    {
        base.Configure(builder);

        builder.ToTable("ItemFamilies");

        builder.HasKey(ifa => ifa.Id);

        builder
            .HasMany(ifa => ifa.Items)
            .WithOne(i => i.ItemFamily);

        builder
            .HasOne(ifa => ifa.ItemFamilyGroup)
            .WithMany(ifag => ifag.ItemFamilies);
    }
}
