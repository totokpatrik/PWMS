namespace PWMS.Infrastructure.Core.Services;

public class CurrentSite : ICurrentSite
{
    [NotNull]
    public Guid? Id { get; set; }
}
