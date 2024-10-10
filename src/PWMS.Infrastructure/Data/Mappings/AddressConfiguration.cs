using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PWMS.Domain.Addresses.Aggregates;
using PWMS.Infrastructure.Data.Extensions;

namespace PWMS.Infrastructure.Data.Mappings;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .ConfigureBaseEntity();

        builder
            .Property(address => address.AddressLine)
            .IsRequired()
            .HasMaxLength(100);
    }
}
