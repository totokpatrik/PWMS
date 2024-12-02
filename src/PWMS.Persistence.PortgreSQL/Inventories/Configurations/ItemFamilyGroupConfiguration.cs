using PWMS.Domain.Inventories.Items.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Persistence.PortgreSQL.Inventories.Configurations;

public class ItemFamilyGroupConfiguration : AuditableWarehouseConfiguration<ItemFamilyGroup>, IEntityTypeConfiguration<ItemFamilyGroup>
{
    public override void Configure(EntityTypeBuilder<ItemFamilyGroup> builder)
    {
        base.Configure(builder);

        builder.ToTable("ItemFamilyGroups");

        builder.HasKey(ifag => ifag.Id);

        builder
            .HasMany(ifag => ifag.ItemFamilies)
            .WithOne(ifa => ifa.ItemFamilyGroup);
    }
}
