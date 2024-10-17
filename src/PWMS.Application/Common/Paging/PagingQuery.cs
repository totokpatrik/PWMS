﻿using PWMS.Application.Common.Paging;

namespace PWMS.Application.Common.Paging;

public class PagingQuery<TM, TF> : IRequest<TM>
    where TF : class, new()
{
    protected PagingQuery(IPageContext<TF> pageContext) => PageContext = pageContext;

    public IPageContext<TF> PageContext { get; set; }
}
