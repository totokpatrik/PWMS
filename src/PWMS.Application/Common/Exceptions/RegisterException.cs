using Microsoft.AspNetCore.Identity;

namespace PWMS.Application.Common.Exceptions;

public class RegisterException : Exception
{
    public RegisterException() : base()
    {
    }
    public RegisterException(IEnumerable<IdentityError> errors)
        : this()
    {
        foreach (var error in errors)
        {
            Errors.Add(new KeyValuePair<string, string>(error.Code, error.Description));
        }
    }
    public IList<KeyValuePair<string, string>> Errors { get; } = new List<KeyValuePair<string, string>>();
}
