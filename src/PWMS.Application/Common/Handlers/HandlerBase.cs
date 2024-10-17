namespace PWMS.Application.Common.Handlers;

using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;

public abstract class HandlerBase<TQ, TM> : IRequestHandler<TQ, TM>
    where TQ : IRequest<TM>
{
    protected ICurrentUserService CurrentUserService { get; }

    protected IMapper Mapper { get; }

    protected HandlerBase(ICurrentUserService currentUserService, IMapper mapper)
    {
        CurrentUserService = currentUserService.ThrowIfNull();
        Mapper = mapper.ThrowIfNull();
    }

    public abstract Task<TM> Handle(TQ request, CancellationToken cancellationToken);
}
