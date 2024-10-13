using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PWMS.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    (IValidator<TRequest> validator)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var response = await next();
            //return Result<TResponse>.Invalid(validationResult.AsErrors());
        }

        return await next();
    }
}
