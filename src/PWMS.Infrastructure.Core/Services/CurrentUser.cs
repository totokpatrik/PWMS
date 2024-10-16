﻿namespace PWMS.Infrastructure.Core.Services;

public sealed class CurrentUser : ICurrentUser
{
    [NotNull]
    public long? Id { get; init; }

    [NotNull]
    public string? FirstName { get; init; }

    [NotNull]
    public string? LastName { get; init; }

    [NotNull]
    public string? MiddleName { get; init; }
}
