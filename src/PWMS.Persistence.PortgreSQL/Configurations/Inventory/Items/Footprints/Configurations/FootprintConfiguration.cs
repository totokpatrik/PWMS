using PWMS.Domain.Configuration.Inventory.Items.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;

namespace PWMS.Persistence.PortgreSQL.Configurations.Inventory.Items.Footprints.Configurations;

public class FootprintConfiguration : AuditableWarehouseConfiguration<Footprint>, IEntityTypeConfiguration<Footprint>
{
    public override void Configure(EntityTypeBuilder<Footprint> builder)
    {
        base.Configure(builder);

        builder.ToTable("Footprints");

        builder.HasKey(i => i.Id);

        builder
            .HasOne(f => f.Item)
            .WithMany(i => i.Footprints);

        builder
            .HasMany(f => f.FootprintDetails)
            .WithOne(fd => fd.Footprint);
    }
}
