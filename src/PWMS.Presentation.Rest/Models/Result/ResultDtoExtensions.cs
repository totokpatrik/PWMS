using FluentResults;

namespace PWMS.Presentation.Rest.Models.Result;

public static class ResultDtoExtensions
{
    public static ResultDto<T> ToResultDto<T>(this Result<T> result) =>
        new(result.Value, result.IsSuccess, TransformErrors(result.Errors));

    private static IEnumerable<ErrorDto> TransformErrors(IEnumerable<IError> errors) =>
        errors.Select(e => new ErrorDto(e.Message, "asd"));
}
