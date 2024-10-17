using PWMS.Application.Common.Interfaces;

namespace PWMS.Application.Common.Handlers;
public abstract class HandlerDbQueryBase<TQ, TM> : HandlerDbBase<TQ, TM>
    where TQ : IRequest<TM>
{
    protected HandlerDbQueryBase(
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService)
        : base(currentUserService, mapper, contextDb)
    {
    }
}
