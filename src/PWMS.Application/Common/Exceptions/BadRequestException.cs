namespace PWMS.Application.Common.Exceptions;

[Serializable]
public sealed class BadRequestException(string message) : Exception(message);
