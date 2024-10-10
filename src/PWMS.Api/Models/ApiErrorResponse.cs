﻿using System.Text.Json.Serialization;

namespace PWMS.Api.Models;

[method: JsonConstructor]
public sealed class ApiErrorResponse(string message)
{
    public string Message { get; } = message;

    public override string ToString() => Message;
}