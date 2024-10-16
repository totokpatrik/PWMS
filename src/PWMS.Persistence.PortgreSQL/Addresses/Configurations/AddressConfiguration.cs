using PWMS.Domain.Addresses.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;

namespace PWMS.Persistence.PortgreSQL.Addresses.Configurations;

public class AddressConfiguration : AuditableConfiguration<Address>, IEntityTypeConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);

        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AddressLine)
            .IsRequired()
            .HasMaxLength(100);
    }
}
