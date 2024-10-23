namespace PWMS.Application.Common.Paging;

public class PagingQuery<TM> : IRequest<TM>
{
    protected PagingQuery(IPageContext pageContext) => PageContext = pageContext;

    public IPageContext PageContext { get; set; }
}
