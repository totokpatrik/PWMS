namespace PWMS.Application.Core.Sites.Queries.GetById;

internal sealed class GetSiteByIdQueryValidator : AbstractValidator<GetSiteByIdQuery>
{
    public GetSiteByIdQueryValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();
    }
}
