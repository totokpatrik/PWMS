﻿namespace PWMS.Application.Common.Exceptions;

[Serializable]
public sealed class AuthorizationException(string message) : Exception(message);
