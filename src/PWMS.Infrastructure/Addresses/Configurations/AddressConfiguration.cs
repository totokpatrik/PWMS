using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Infrastructure.Addresses.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AddressLine).HasMaxLength(100).IsRequired();
    }
}
