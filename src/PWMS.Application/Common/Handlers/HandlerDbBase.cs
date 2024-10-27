namespace PWMS.Application.Common.Handlers;

using PWMS.Application.Common.Interfaces;

public abstract class HandlerDbBase<TQ, TM> : HandlerBase<TQ, TM>
    where TQ : IRequest<TM>
{

    protected HandlerDbBase(ICurrentUserService currentUserService,
        IMapper mapper,
        IApplicationDbContext contextDb)
        : base(currentUserService, mapper) { }

    public abstract override Task<TM> Handle(TQ request, CancellationToken cancellationToken);
}
