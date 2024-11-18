using PWMS.Domain.Core.Sites.Entities;
using PWMS.Persistence.PortgreSQL.Common.Configurations;

namespace PWMS.Persistence.PortgreSQL.Core.Sites.Configurations;

public class SiteConfiguration : AuditableConfiguration<Site>, IEntityTypeConfiguration<Site>
{
    public override void Configure(EntityTypeBuilder<Site> builder)
    {
        base.Configure(builder);

        builder.ToTable("Sites");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.Warehouses)
            .WithOne(x => x.Site);

        builder.HasMany(x => x.Users)
            .WithMany(x => x.UserSites)
            .UsingEntity(join => join.ToTable("SitesUsers"));

        builder.HasMany(x => x.Admins)
            .WithMany(x => x.AdminSites)
            .UsingEntity(join => join.ToTable("SitesAdmins"));

        builder.HasOne(x => x.Owner)
            .WithMany();
    }
}