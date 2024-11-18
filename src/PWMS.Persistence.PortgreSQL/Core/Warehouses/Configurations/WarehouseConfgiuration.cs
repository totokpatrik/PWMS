using PWMS.Domain.Core.Warehouses.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;

namespace PWMS.Persistence.PortgreSQL.Core.Warehouses.Configurations;

public class WarehouseConfgiuration : AuditableConfiguration<Warehouse>, IEntityTypeConfiguration<Warehouse>
{
    public override void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        base.Configure(builder);

        builder.ToTable("Warehouses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(x => x.Site)
            .WithMany(x => x.Warehouses);

        builder.HasMany(x => x.Users)
            .WithMany(x => x.UserWarehouses)
            .UsingEntity(join => join.ToTable("WarehousesUsers"));

        builder.HasMany(x => x.Admins)
            .WithMany(x => x.AdminWarehouses)
            .UsingEntity(join => join.ToTable("WarehousesAdmins"));

        builder.HasOne(x => x.Owner)
            .WithMany();
    }
}
