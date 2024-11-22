using PWMS.Domain.Auth.Entities;

namespace PWMS.Persistence.PortgreSQL.Auth.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.SelectedSite)
            .WithMany(s => s.UsersSelected);

        builder.HasOne(u => u.SelectedWarehouse)
            .WithMany();
    }
}
