namespace PWMS.Application.Common.Filters;

public sealed record FilterFieldDefinition<T>
{
    public T? Value { get; init; }
}