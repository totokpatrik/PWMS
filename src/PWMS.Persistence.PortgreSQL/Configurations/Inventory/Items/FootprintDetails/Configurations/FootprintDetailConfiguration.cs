using PWMS.Domain.Configuration.Inventory.Items.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;

namespace PWMS.Persistence.PortgreSQL.Configurations.Inventory.Items.FootprintDetails.Configurations;

public class FootprintDetailConfiguration : AuditableWarehouseConfiguration<FootprintDetail>, IEntityTypeConfiguration<FootprintDetail>
{
    public override void Configure(EntityTypeBuilder<FootprintDetail> builder)
    {
        base.Configure(builder);

        builder.ToTable("FootprintDetails");

        builder.HasKey(fd => fd.Id);

        builder
            .HasOne(fd => fd.Footprint)
            .WithMany(f => f.FootprintDetails);

        builder
            .HasOne(fd => fd.UnitOfMeasure)
            .WithMany();
    }
}
