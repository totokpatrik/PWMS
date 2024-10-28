using System.Diagnostics.CodeAnalysis;

namespace PWMS.Application.Common.Exceptions;

[Serializable]
public sealed class NotFoundException : Exception
{
    public NotFoundException()
        : base()
    {
    }

    [ExcludeFromCodeCoverage]
    public NotFoundException(string message)
        : base(message)
    {
    }

    [ExcludeFromCodeCoverage]
    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
