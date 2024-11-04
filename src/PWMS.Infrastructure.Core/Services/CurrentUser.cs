namespace PWMS.Infrastructure.Core.Services;

public sealed class CurrentUser : ICurrentUser
{
    [NotNull]
    public string? Id { get; init; }
    [NotNull]
    public string? Username { get; init; }
}
