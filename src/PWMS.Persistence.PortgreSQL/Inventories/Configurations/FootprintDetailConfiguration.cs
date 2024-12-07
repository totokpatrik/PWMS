using PWMS.Domain.Configuration.Inventory.Items.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Persistence.PortgreSQL.Inventories.Configurations;

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
