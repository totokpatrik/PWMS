namespace PWMS.Application.Common.Paging;

public class PagingQueryValidator<T, TCM> : AbstractValidator<T>
    where T : PagingQuery<TCM>
{
    protected PagingQueryValidator()
    {
        RuleFor(x => x.PageContext).NotNull()
            .NotEmpty().WithMessage("PageContext is required.");

        RuleFor(x => x.PageContext.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("PageIndex at least greater than or equal to 1.");

        RuleFor(x => x.PageContext.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
