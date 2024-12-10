using PWMS.Domain.Inventories.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;

namespace PWMS.Persistence.PortgreSQL.Inventories.Configurations;

public class UnitOfMeasureConfiguration : AuditableWarehouseConfiguration<UnitOfMeasure>, IEntityTypeConfiguration<UnitOfMeasure>
{
    public override void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        base.Configure(builder);


        builder.ToTable("UnitOfMeasures");

        builder.HasKey(a => a.Id);

        builder.HasMany(uom => uom.FootprintDetails)
            .WithOne(fd => fd.UnitOfMeasure);

        builder.HasMany(uom => uom.AlternateItems)
            .WithOne(ai => ai.UnitOfMeasure);
    }
}
