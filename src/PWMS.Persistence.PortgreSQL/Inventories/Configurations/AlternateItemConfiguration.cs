using PWMS.Domain.Configuration.Inventory.Items.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;

namespace PWMS.Persistence.PortgreSQL.Inventories.Configurations;

public class AlternateItemConfiguration : AuditableWarehouseConfiguration<AlternateItem>, IEntityTypeConfiguration<AlternateItem>
{
    public override void Configure(EntityTypeBuilder<AlternateItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("AlternateItems");

        builder.HasKey(ai => ai.Id);

        builder
            .HasOne(ai => ai.UnitOfMeasure)
            .WithMany(uom => uom.AlternateItems);

        builder.Property(ai => ai.AlternateItemType)
            .HasConversion<int>();
    }
}
