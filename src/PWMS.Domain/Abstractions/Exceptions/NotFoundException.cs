namespace PWMS.Domain.Abstractions.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name) : base(name)
    {

    }
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.") { }
}
