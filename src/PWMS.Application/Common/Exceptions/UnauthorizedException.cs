namespace PWMS.Application.Common.Exceptions;

[Serializable]
public sealed class UnauthorizedException(string message) : Exception(message)
{

}
