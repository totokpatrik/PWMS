using PWMS.Domain.Common;

namespace PWMS.Persistence.PortgreSQL.Common.Configurations;

public class AuditableWarehouseConfiguration<T> where T : class, IBaseAuditableWarehouseEntity<string>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.Created);

        builder.Property(e => e.CreatedBy);

        builder.Property(e => e.Modified);

        builder.Property(e => e.ModifiedBy);

        builder.Property(e => e.WarehouseId);
    }
}
