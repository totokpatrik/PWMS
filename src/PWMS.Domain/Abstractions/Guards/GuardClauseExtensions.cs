﻿using PWMS.Domain.Abstractions.Exceptions;

namespace PWMS.Domain.Abstractions.Guards;

public static partial class GuardClauseExtensions
{
    private static void Error(string message)
    {
        throw new DomainException(message);
    }

    private static void NotFound(string message)
    {
        throw new NotFoundException(message);
    }
}