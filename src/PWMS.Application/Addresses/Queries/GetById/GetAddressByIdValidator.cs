namespace PWMS.Application.Addresses.Queries.GetById;

internal sealed class GetAddressByIdValidator : AbstractValidator<GetAddressByIdQuery>
{
    public GetAddressByIdValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty();
    }
}
