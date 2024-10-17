using PWMS.Domain.Common;

namespace PWMS.Persistence.PortgreSQL.Common.Configurations;

public class AuditableConfiguration<T> where T : class, IBaseAuditableEntity<string>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.Created);

        builder.Property(e => e.CreatedBy);

        builder.Property(e => e.Modified);

        builder.Property(e => e.ModifiedBy);
    }
}
