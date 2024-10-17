using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;

namespace PWMS.Application.Common.Handlers;
public abstract class PagingDbQueryHandlerDb<TQ, TCM, TM> : HandlerDbQueryBase<TQ, TCM>
    where TQ : IRequest<TCM>
    where TCM : Result<CollectionViewModel<TM>>, new()
{
    protected PagingDbQueryHandlerDb(IApplicationDbContext applicationDbContext, IMapper mapper,
        ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }

    public abstract override Task<TCM> Handle(TQ request, CancellationToken cancellationToken);
}
