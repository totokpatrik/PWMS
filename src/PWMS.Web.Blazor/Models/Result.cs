namespace PWMS.Web.Blazor.Models;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public IEnumerable<Error>? Errors { get; set; }
    public T Data { get; set; } = default!;

}
