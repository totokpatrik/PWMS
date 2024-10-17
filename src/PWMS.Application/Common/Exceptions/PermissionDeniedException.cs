namespace PWMS.Application.Common.Exceptions;

[Serializable]
public sealed class PermissionDeniedException(string message) : Exception(message);
