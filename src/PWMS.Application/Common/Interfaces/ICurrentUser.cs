namespace PWMS.Application.Common.Interfaces;

public interface ICurrentUser
{
    string? Id { get; }
    string? Username { get; }
}
